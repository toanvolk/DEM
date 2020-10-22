using AutoMapper;
using DEM.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public class DashboardService : IDashboardService
    {
        private ILogger<DashboardService> _logger { get; set; }
        private readonly IUnitOfWorkMedia _unitOfWorfkMedia;
        private readonly IMapper _mapper;
        public DashboardService(ILogger<DashboardService> logger, IUnitOfWorkMedia unitOfWorkMedia, IMapper mapper)
        {
            _logger = logger;
            _unitOfWorfkMedia = unitOfWorkMedia;
            _mapper = mapper;
        }
        public ICollection<DailyInMonthCurrentDto> GetDailyInMonthCurrent()
        {
            //_unitOfWorfkMedia.ExpenseRepository.GetDynamicResult
            throw new NotImplementedException();
        }
    }
}
