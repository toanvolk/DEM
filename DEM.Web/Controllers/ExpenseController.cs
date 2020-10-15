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
        public JsonResult LoadData(Guid categoryId)
        {
            var model = _expenseService.LoadData(categoryId);
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
