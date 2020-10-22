using AutoMapper;
using DEM.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public class HomeService : BaseService, IHomeService
    {
        private ILogger<HomeService> _logger { get; set; }
        private readonly IUnitOfWorkMedia _unitOfWorfkMedia;
        private readonly IMapper _mapper;
        public HomeService(ILogger<HomeService> logger, IUnitOfWorkMedia unitOfWorkMedia, IMapper mapper) :base (logger, unitOfWorkMedia, mapper)
        {
            _logger = logger;
            _unitOfWorfkMedia = unitOfWorkMedia;
            _mapper = mapper;
        }

        public Tuple<ICollection<string>, ICollection<string>> GetDailyInMonthCurrent()
        {
            var datas = _unitOfWorfkMedia.GetDynamicResult("sp_DailyInMonthCurrent_Dashboard");
            throw new NotImplementedException();
        }
    }
}
