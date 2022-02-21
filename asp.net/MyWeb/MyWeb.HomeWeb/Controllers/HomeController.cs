using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWeb.HomeWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
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

        //[Authorize]
        public IActionResult TicketList()
        {
            // if (!User.Identity.IsAuthenticated) exception

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
        public IActionResult TicketUpdate([FromBody] TicketModel model)
        {
            //var model = new TicketModel()
            //{
            //    TicketID = ticketId,
            //    Title = title
            //};


            //var sr = new StreamReader(Request.Body);
            //var body = sr.ReadToEndAsync().GetAwaiter().GetResult();
            

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

            return Json(model);
        }

        public IActionResult BoardList(string search)
        {
            return View(BoardModel.GetList(search));
        }

        [Authorize]
        public IActionResult BoardWrite()
        {
            return View();
        }

        [Authorize]
        public IActionResult BoardWriteInput([FromForm] BoardModel model)
        {
            model.RegUser = Convert.ToUInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            model.RegUsername = User.Identity.Name;
            model.Insert();
            return Redirect("/home/boardlist");
        }

        [Authorize]
        public IActionResult BoardEdit(uint idx, string type)
        {
            var model = BoardModel.Get(idx);
            var userSeq = Convert.ToUInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (model.RegUser != userSeq)
                throw new Exception("수정 권한이 없습니다.");

            if (type == "U")
            {
                return View(model);
            }
            else if (type == "D")
            {
                model.Delete();
                return Redirect("/home/boardlist");
            }

            throw new Exception("잘못된 요청 입니다.");
        }

        [Authorize]
        public IActionResult BoardEditInput([FromForm] BoardModel model)
        {
            var findModel = BoardModel.Get(model.Idx);

            findModel.Title = model.Title;
            findModel.Contents = model.Contents;
            findModel.Update();

            return Redirect($"/home/boardview?idx={findModel.Idx}");
        }

        public IActionResult BoardView(uint idx)
        {
            return View(BoardModel.Get(idx));
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
