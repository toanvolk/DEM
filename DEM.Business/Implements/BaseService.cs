using AutoMapper;
using DEM.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public class BaseService : IBaseService
    {
        private ILogger<BaseService> _logger { get; set; }
        private readonly IUnitOfWorkMedia _unitOfWorfkMedia;
        private readonly IMapper _mapper;
        public BaseService(ILogger<BaseService> logger, IUnitOfWorkMedia unitOfWorkMedia, IMapper mapper)
        {
            _logger = logger;
            _unitOfWorfkMedia = unitOfWorkMedia;
            _mapper = mapper;
        }
        public string GetDatabaseName() => _unitOfWorfkMedia.GetDatabaseName();
    }
}
