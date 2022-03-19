using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeb.HomeWeb.Utils
{
    public class AsciiHelper
    {
        private static readonly string[] ASCII = new string[] { "$", "@", "B", "%", "8", "&", "W", "M", "#", "*", "o", "a", "h", "k", "b", "d", "p", "q", "w", "m", "Z", "O", "0", "Q", "L", "C", "J", "U", "Y", "X", "z", "c", "v", "u", "n", "x", "r", "j", "f", "t", "/", "\\", "|", "(", ")", "1", "{", "}", "[", "]", "?", "-", "_", "+", "~", "<", ">", "i", "!", "l", "I", ";", ":", ",", "\"", "^", "`", "'", ".", " " };

        public static string ConvertImageToAscii(Bitmap bmp, int width)
        {
            StringBuilder sb = new StringBuilder();

            double per = (double)width / bmp.Width;
            var newHeight = (int)Math.Round(bmp.Height * per);

            using (var newBmp = new Bitmap(bmp, new Size(width, newHeight)))
            {
                for (int x = 0; x < newBmp.Height; x++)
                {
                    for (int y = 0; y < newBmp.Width; y++)
                    {
                        var color = newBmp.GetPixel(y, x);
                        var bright = GetBrightness(color);
                        var idx = (bright / 255 * ASCII.Length - 1);
                        if (idx < 0)
                            idx = ASCII.Length - 1;
                        var pxl = ASCII[(int)Math.Round(idx)];
                        sb.Append(pxl);
                    }
                    sb.AppendLine();
                }
            } 

            return sb.ToString();
        }

        private static double GetBrightness(Color color)
        {
            return Math.Sqrt(Math.Pow(color.R, 2) * 0.241 +
                             Math.Pow(color.G, 2) * 0.691 +
                             Math.Pow(color.B, 2) * 0.068);
        }
    }
}
