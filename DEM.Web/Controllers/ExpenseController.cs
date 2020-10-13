using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEM.App;
using Microsoft.AspNetCore.Mvc;

namespace DEM.Web.Controllers
{
    public class ExpenseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add(Guid categoryId)
        {
            var payer = new MasterAppData().Payers;
            var model = new Tuple<Guid,List<Payer>>(categoryId, payer);
            return PartialView("_add", model);
        }
    }
}
