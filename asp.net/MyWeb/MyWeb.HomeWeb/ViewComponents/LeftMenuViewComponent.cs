using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWeb.HomeWeb.Controllers.ViewComponents
{
    public class LeftMenuViewComponent : ViewComponent
    {
        public LeftMenuViewComponent()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("index");
        }
    }
}
