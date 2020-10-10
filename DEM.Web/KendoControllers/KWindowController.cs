using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEM.App.Kendo;
using Microsoft.AspNetCore.Mvc;

namespace DEM.Web.KendoControllers
{
    public class KWindowController : Controller
    {
        public IActionResult Index(WindowDto windowDto)
        {
            return View(windowDto);
        }
        public IActionResult Normal()
        {
            return View();
        }
    }
}
