using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using MyWeb.HomeWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyWeb.HomeWeb.Controllers
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

        public IActionResult TicketList()
        {
            var status = "In Progress";
            return View(TicketModel.GetList(status));
            //using (var conn = new MySqlConnection(@"Server=192.168.0.105;Port=8448;Database=myweb;Uid=chanos-dev;Pwd=!Qweasdzxc123;"))
            //{
            //    conn.Open();

            //    var status = "In Progress";
            //    var sql = "select ticket_id TicketID, title Title, status Status from t_ticket where status = @status order by ticket_id";
            //    var items = conn.Query<TicketModel>(sql, param: new { status });
            //    ViewData["list"] = items;

            //    //SqlMapper.Query<TicketModel>(conn, sql, new { status }).ToList(); 
            //    return View(items);
            //} 
            ////return View();
        }

        [HttpPost]
        //public IActionResult TicketUpdate(int ticketId, string title)
        public IActionResult TicketUpdate([FromForm] TicketModel model)
        {
            //var model = new TicketModel()
            //{
            //    TicketID = ticketId,
            //    Title = title
            //};
            model.Update();
//            using (var conn = new MySqlConnection(@"Server=192.168.0.105;Port=8448;Database=myweb;Uid=chanos-dev;Pwd=!Qweasdzxc123;"))
//            {
//                conn.Open();
//                using (var cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = @"
//update t_ticket
//set title = @title
//where ticket_id = @ticket_id
//";
//                    cmd.Parameters.AddWithValue("@title", title);
//                    cmd.Parameters.AddWithValue("@ticket_id", ticketId);

//                    cmd.ExecuteNonQuery();
//                }
//            }

            //return Json(new { msg = "OK" });
            return Redirect("/home/ticketlist");
        }

        public IActionResult Chat()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
