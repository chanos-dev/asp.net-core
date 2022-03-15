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

        public static string ConvertImageToAscii(Bitmap img)
        {
            StringBuilder sb = new StringBuilder();

            for(int x = 0; x < img.Height; x++)
            {
                for(int y = 0; y < img.Width; y++)
                {
                    var color = img.GetPixel(y, x);
                    var bright = GetBrightness(color);
                    var idx = (bright / 255 * ASCII.Length - 1);
                    if (idx < 0)
                        idx = ASCII.Length - 1;
                    var pxl = ASCII[(int)Math.Round(idx)];
                    sb.Append(pxl);
                }
                sb.AppendLine();
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
