using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEM.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DEM.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRootCategoryService _rootCategoryService;
        private readonly ICategoryService _categoryService;
        public CategoryController(ILogger<HomeController> logger, IRootCategoryService rootCategoryService, ICategoryService categoryService)
        {
            _logger = logger;
            _rootCategoryService = rootCategoryService;
            _categoryService = categoryService;
        }
        public IActionResult Index(string rootCategoryType)
        {
            var categorys = _categoryService.LoadDatas(rootCategoryType);
            var model = new Tuple<List<CategoryDto>, string>(categorys, rootCategoryType);
            return View(model);
        }

        public IActionResult Add()
        {
            return PartialView("_add");
        }

        [HttpPost]
        public JsonResult Create(CategoryDto category)
        {
            try
            {
                if (_categoryService.Add(category))
                {
                    var response = new DataResponeCommon() { Statu = StatuCodeEnum.OK, Message = "Thêm thành công" };
                    return Json(response);
                }
                else
                {
                    var response = new DataResponeCommon() { Statu = StatuCodeEnum.InternalServerError, Message = "Thêm thất bại" };
                    return Json(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }


        }

        public JsonResult LoadDatas(string rootCategoryType)
        {
            var model = _categoryService.LoadDatas(rootCategoryType);
            var response = new DataResponeCommon<List<CategoryDto>>()
            {
                Data = model,
                Message = "OK",
                Statu = StatuCodeEnum.OK
            };
            return Json(response);
        }
    }
}
