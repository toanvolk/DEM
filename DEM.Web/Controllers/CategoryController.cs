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
            var categorys = _categoryService.LoadDatas(rootCategoryType, true);
            var model = new Tuple<List<CategoryDto>, string>(categorys, rootCategoryType);
            return View(model);
        }

        public IActionResult Add(string rootCategoryType)
        {
            return PartialView("_add",rootCategoryType);
        }
        public IActionResult EditForm(Guid categoryId)
        {
            var categoryDto = _categoryService.FindId(categoryId);
            return PartialView("_edit", categoryDto);
        }
        public IActionResult Edit(CategoryDto category)
        {
            try
            {
                if (_categoryService.Edit(category))
                {
                    var response = new DataResponeCommon() { Statu = StatuCodeEnum.OK, Message = "Cập nhật thành công" };
                    return Json(response);
                }
                else
                {
                    var response = new DataResponeCommon() { Statu = StatuCodeEnum.InternalServerError, Message = "Cập nhật thất bại" };
                    return Json(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
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
        [HttpPost]
        public JsonResult Delete(Guid categoryId)
        {
            try
            {
                if (_categoryService.Delete(categoryId))
                {
                    var response = new DataResponeCommon() { Statu = StatuCodeEnum.OK, Message = "Xóa thành công" };
                    return Json(response);
                }
                else
                {
                    var response = new DataResponeCommon() { Statu = StatuCodeEnum.InternalServerError, Message = "Xóa thất bại" };
                    return Json(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
        [HttpPost]
        public JsonResult ChangeStatu(Guid categoryId, bool notUse)
        {
            try
            {
                if (_categoryService.ChangeStatu(categoryId, notUse))
                {
                    var response = new DataResponeCommon() { Statu = StatuCodeEnum.OK, Message = "Cập nhật statu thành công" };
                    return Json(response);
                }
                else
                {
                    var response = new DataResponeCommon() { Statu = StatuCodeEnum.InternalServerError, Message = "Cập nhật statu thất bại" };
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
            var model = _categoryService.LoadDatas(rootCategoryType, true);
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
