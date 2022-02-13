using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWeb.HomeWeb.Controllers.ViewComponents
{
    public class DashBoardTitleViewComponent : ViewComponent
    {
        public DashBoardTitleViewComponent()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("index");
        }
    }
}
