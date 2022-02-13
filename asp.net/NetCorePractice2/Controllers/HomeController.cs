using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCorePractice2.Models;

namespace NetCorePractice2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Test(string x, string y)
        {
            //Request.Form
            //Request.Query 

            //var model = new TestModel()
            //{
            //    X = 999,
            //    Y = "test",
            //};
            //return Json(model);

            //return Content("test", "text/html");
            //return PartialView();
            //return File("D:\test.xls", "application/octet-stream", "test.xls");


            ViewData["x"] = x;
            ViewBag.y = y;
            ViewBag.z = "zzz";

            List<TestModel> list = new List<TestModel>()
            {
                new TestModel() {X = 1, Y = "A"},
                new TestModel() {X = 2, Y = "B"},
                new TestModel() {X = 3, Y = "C"},
                new TestModel() {X = 4, Y = "D"},
            };

            //ViewData["list"] = list;

            return View(list);
        }     
    }
}
