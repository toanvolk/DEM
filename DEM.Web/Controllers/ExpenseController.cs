using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEM.App;
using DEM.EF;
using Microsoft.AspNetCore.Mvc;

namespace DEM.Web.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;        
        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
            
        }
        public IActionResult Index(string categoryId)
        {
            var model = new Tuple<string>(categoryId);
            return View(model);
        }
        public IActionResult Add(Guid categoryId)
        {
            var payer = _expenseService.GetPayers();
            var model = new Tuple<Guid,List<Payer>>(categoryId, payer.ToList());
            return PartialView("_add", model);
        }
        public IActionResult Edit(Guid expenseId)
        {
            var expenseDto = _expenseService.GetData(expenseId);
            var payers = _expenseService.GetPayers();
            var model = new Tuple<ExpenseDto, ICollection<Payer>>(expenseDto, payers);
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
        public JsonResult LoadData(Guid categoryId, DateTime startTime, DateTime endTime)
        {
            var model = _expenseService.LoadData(categoryId, startTime, endTime);
            var response = new DataResponeCommon<List<ExpenseDto>>()
            {
                Data = model,
                Message = "OK",
                Statu = StatuCodeEnum.OK
            };
            return Json(response);
        }
    }
}
