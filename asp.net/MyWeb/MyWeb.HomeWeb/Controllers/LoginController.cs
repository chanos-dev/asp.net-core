using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.HomeWeb.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace MyWeb.HomeWeb.Controllers
{
    public class LoginController : Controller
    {
        public LoginController()
        {

        }

        public IActionResult Index()
        {
            return Redirect("/login/login");
        }

        [HttpGet]
        public IActionResult Login(string msg)
        {
            ViewData["msg"] = msg;
            return View();
        }  


        [HttpPost]
        [Route("/login/login")]
        public async Task<IActionResult> LoginProc([FromForm] UserModel user)
        {
            try
            {
                var login = user.GetLoginUser();

                // 인증
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, login.UserSeq.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, login.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Email, login.Email));
                // 사용자 kv 추가
                identity.AddClaim(new Claim("LastCheckDateTime", DateTime.UtcNow.ToString("yyyyMMddHHmmss")));

                if (login.UserName == "chanos")
                    identity.AddClaim(new Claim(ClaimTypes.Role, "GEUST"));

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    // 영속성, 로그인 유지할 것인지
                    IsPersistent = false,
                    // 쿠키 유지 시간
                    ExpiresUtc = DateTime.UtcNow.AddHours(4),
                    // 갱신
                    AllowRefresh = true,
                });

                return Redirect("/");
            }
            catch(Exception ex)
            {
                return Redirect($"/login/login?msg={HttpUtility.UrlEncode(ex.Message)}");
            }
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Register(string msg)
        {
            ViewData["msg"] = msg;
            return View();
        }

        [HttpPost]
        [Route("/login/register")]
        public IActionResult RegisterProc([FromForm] UserModel user)
        {
            try
            {
                var check = Request.Form["passwordCheck"];

                if (user.PassWord != check)
                    throw new Exception("Password가 서로 다릅니다.");

                user.Register();
                return Redirect("/login/login");
            }
            catch (Exception ex)
            {
                return Redirect($"/login/register?msg={HttpUtility.UrlEncode(ex.Message)}");
            }
        }
    }
}
