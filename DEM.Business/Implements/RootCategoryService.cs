using AutoMapper;
using DEM.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public class RootCategoryService : BaseService, IRootCategoryService
    {
        private ILogger<RootCategoryService> _logger { get; set; }
        private readonly IUnitOfWorkMedia _unitOfWorfkMedia;
        private readonly IMapper _mapper;
        public RootCategoryService(ILogger<RootCategoryService> logger, IUnitOfWorkMedia unitOfWorkMedia, IMapper mapper) : base(logger, unitOfWorkMedia, mapper)
        {
            _logger = logger;
            _unitOfWorfkMedia = unitOfWorkMedia;
            _mapper = mapper;
        }
        public List<RootCategoryDto> GetDatas()
        {
            var datas = new List<RootCategoryDto>();
            var rootCategorys = Enum.GetValues(typeof(RootCategoryEnum));
            foreach (var item in rootCategorys)
            {
                string caption = "", name = "";
                switch (item)
                {
                    case RootCategoryEnum.EXPENSE:
                        caption = "CHI PHÍ";
                        name = Enum.GetName(typeof(RootCategoryEnum), RootCategoryEnum.EXPENSE);
                        break;
                    case RootCategoryEnum.REVENUE:
                        caption = "THU";
                        name = Enum.GetName(typeof(RootCategoryEnum), RootCategoryEnum.REVENUE);
                        break;
                    case RootCategoryEnum.SAVING:
                        caption = "TIẾT KIỆM";
                        name = Enum.GetName(typeof(RootCategoryEnum), RootCategoryEnum.SAVING);
                        break;
                    default:
                        caption = "";
                        name = "";
                        break;
                }
                datas.Add(new RootCategoryDto() { Name = name, Caption = caption });
            }

            return datas;
        }
    }
}
