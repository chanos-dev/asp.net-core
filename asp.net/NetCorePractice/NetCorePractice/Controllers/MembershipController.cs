using Microsoft.AspNetCore.Mvc;
using NetCore.Data.ViewModels;
using NetCore.Services.Interfaces;
using NetCore.Services.Svcs;
using NetCorePractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePractice.Controllers
{
    public class MembershipController : Controller
    {
        private IUser UserService { get; set; }

        public MembershipController(IUser userService)
        {
            this.UserService = userService;
        } 

        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Login(LoginInfo loginInfo)
        {
            var message = string.Empty;

            if (ModelState.IsValid)
            { 
                if (UserService.MatchUserInfo(loginInfo))
                {
                    TempData["Message"] = "로그인이 성공적으로 이루어졌습니다.";
                    return RedirectToAction("Index", "Membership");
                }
                else
                {
                    message = "로그인에 실패 했습니다.";
                }
            }
            else
            {
                message = "로그인 정보를 올바르게 입력하세요.";
            }

            ModelState.AddModelError(string.Empty, message);
            return View(loginInfo);
        }
    }
}
