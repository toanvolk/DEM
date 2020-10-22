using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DEM.Web.Models;
using DEM.App;

namespace DEM.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;
        private readonly IRootCategoryService _rootCategoryService;
        private readonly ICategoryService _categoryService;
        public HomeController(ILogger<HomeController> logger, IRootCategoryService rootCategoryService, ICategoryService categoryService, IHomeService homeService)
        {
            _logger = logger;
            _rootCategoryService = rootCategoryService;
            _categoryService = categoryService;
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            string providerName = _homeService.GetDatabaseName();
            var model = new Tuple<List<RootCategoryDto>, string>(_rootCategoryService.GetDatas(), providerName);
            return View(model);
        }
        public JsonResult LoadDatas(string rootCategoryType)
        {
            var model = _categoryService.LoadDatas(rootCategoryType, false);
            var response = new DataResponeCommon<List<CategoryDto>>()
            {
                Data = model,
                Message = "OK",
                Statu = StatuCodeEnum.OK
            };
            return Json(response);
        }
        public JsonResult GetDailyInMonthCurrent_Dashboard()
        {
            var model = _homeService.GetDailyInMonthCurrent();
            return Json(new
            {
                Dailys= model.Item1,
                Moneys = model.Item2
            }
            );
        }
    }
}
