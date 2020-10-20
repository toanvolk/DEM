using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEM.App;
using Microsoft.AspNetCore.Mvc;

namespace DEM.Web.Controllers
{
    public class IntendedController : Controller
    {
        private readonly IIntendedService _intendedService;
        public IntendedController(IIntendedService intendedService)
        {
            _intendedService = intendedService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add(string rootCategory)
        {
            var categorys = _intendedService.GetCategories(rootCategory);
            var model = new Tuple<ICollection<CategoryDto>>(categorys);
            return PartialView("_add", model);
        }
        public JsonResult Create(IntendedDto data)
        {
            var response = new DataResponeCommon();
            try
            {
                if (_intendedService.Create(data))
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
    }
}
