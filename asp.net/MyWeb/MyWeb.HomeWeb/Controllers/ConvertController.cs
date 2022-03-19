using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWeb.HomeWeb.Models.Login;
using MyWeb.HomeWeb.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace MyWeb.HomeWeb.Controllers
{
    public class ConvertController : Controller
    {
        private IWebHostEnvironment Environment { get; set; }
        public ConvertController(IWebHostEnvironment environment)
        {
            this.Environment = environment;            
        }
         
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        } 

        [HttpPost]
        public async Task<IActionResult> Converter(IFormFile file, int width)
        {
            if (file != null && file.Length > 0)
            {
                var imagePath = @"\upload\images";
                var uploadPath = $"{Environment.WebRootPath}{imagePath}";

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var uniq = Guid.NewGuid().ToString();
                var filename = $"{uniq}{Path.GetExtension(file.FileName)}";
                var fullPath = Path.Combine(uploadPath, filename);

                imagePath += @"\";
                var filePath = $"..{Path.Combine(imagePath,filename)}";
                
                using (var fs = System.IO.File.Create(fullPath))
                {
                    await file.CopyToAsync(fs);

                    using (var img = Image.FromStream(fs))
                    using (var bmp = new Bitmap(img))
                    {
                        ViewData["convert"] = AsciiHelper.ConvertImageToAscii(bmp, width);
                    }
                } 

                ViewData["path"] = filePath;                
            }
            return View("index");
        }
    }
}
