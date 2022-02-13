using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using MyWeb.HomeWeb.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace MyWeb.HomeWeb.Controllers
{
    // 컨트롤러 접근 시 인증된 사용자만
    [Authorize]
    public class AdminController : Controller
    {
        public AdminController()
        {

        }

        [Authorize(Roles = "ADMIN,USER,GEUST")]
        public IActionResult GetCheck()
        {
            if (User.IsInRole("ADMIN"))
            {
                return Json(new { a = new[] { 3, 6, 9 } });
            }

            return Json(new { a = -1 });
        }

        // 익명 사용자도 가능
        [AllowAnonymous]
        public IActionResult GetUserCheck()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Json(new { a = new[] { 7, 7, 7 } });
            }

            return Json(new { a = -1 });
        }
    }
}
