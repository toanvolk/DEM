using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEM.App;
using DEM.EF;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace DEM.Web.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;        
        private readonly ICategoryService _categoryService;        
        public ExpenseController(IExpenseService expenseService, ICategoryService categoryService)
        {
            _expenseService = expenseService;
            _categoryService = categoryService;
        }
        public IActionResult Index(string rootCategoryType)
        {
            var model = new Tuple<string>(rootCategoryType);
            return View(model);
        }
        public IActionResult Add(string categoryType)
        {
            var payer = _expenseService.GetPayers();
            var rootCategoryEnum = (RootCategoryEnum)Enum.Parse(typeof(RootCategoryEnum), categoryType);
            var categorys = _categoryService.LoadDatas(Enum.GetName(typeof(RootCategoryEnum), rootCategoryEnum), false);
            var model = new Tuple<List<Payer>, List<CategoryDto>>(payer.ToList(), categorys);
            return PartialView("_add", model);
        }
        public IActionResult Edit(Guid expenseId)
        {
            var expenseDto = _expenseService.GetData(expenseId);

            var payers = _expenseService.GetPayers();

            var category = _categoryService.FindId(expenseDto.CategoryId.GetValueOrDefault());
            var categorys = _categoryService.LoadDatas(Enum.GetName(typeof(RootCategoryEnum), category.Type), false);

            var model = new Tuple<ExpenseDto, ICollection<Payer>, List<CategoryDto>>(expenseDto, payers, categorys);
            return PartialView("_edit", model);
        }
        [HttpPost]
        public JsonResult Create(List<ExpenseDto> data)
        {
            var response = new DataResponeCommon();
            try
            {
                if (_expenseService.Create(data))
                {
                    response.Statu = StatuCodeEnum.OK;
                    response.Message = "Tạo thành công!";
                }
                else
                {
                    response.Statu = StatuCodeEnum.InternalServerError;
                    response.Message = "Tạo không thành công!";
                }
            }
            catch (Exception e)
            {
                response.Statu = StatuCodeEnum.InternalServerError;
                response.Message = e.Message;
            }
            return Json(response);
        }

        [HttpPost]
        public JsonResult Update(ExpenseDto data)
        {
            var response = new DataResponeCommon();
            try
            {
                if (_expenseService.Update(data))
                {
                    response.Statu = StatuCodeEnum.OK;
                    response.Message = "Cập nhật thành công!";
                }
                else
                {
                    response.Statu = StatuCodeEnum.InternalServerError;
                    response.Message = "Cập nhật không thành công!";
                }
            }
            catch (Exception e)
            {
                response.Statu = StatuCodeEnum.InternalServerError;
                response.Message = e.Message;
            }
            return Json(response);
        }

        [HttpPost]
        public JsonResult Delete(Guid expenseId)
        {
            try
            {
                if (_expenseService.Delete(expenseId))
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
                throw;
            }
        }
        public JsonResult LoadData(RootCategoryEnum rootCategoryType, DateTime startTime, DateTime endTime, [DataSourceRequest] DataSourceRequest request)
        {
            var model = _expenseService.LoadData(rootCategoryType, startTime, endTime, request.Page, request.PageSize);
            var response = new DataResponeCommon<List<ExpenseDto>>()
            {
                Data = model.Item1,
                Total = model.Item2,
                Message = "OK",
                Statu = StatuCodeEnum.OK
            };
            return Json(response);
        }
    }
}
