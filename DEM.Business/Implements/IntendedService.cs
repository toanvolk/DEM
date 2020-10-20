using AutoMapper;
using DEM.EF;
using DEM.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public class IntendedService : IIntendedService
    {
        private ILogger<IntendedService> _logger { get; set; }
        private readonly IUnitOfWorkMedia _unitOfWorfkMedia;
        private readonly IMapper _mapper;

        private readonly ICategoryService _categoryService;
        public IntendedService(ILogger<IntendedService> logger, IUnitOfWorkMedia unitOfWorkMedia, IMapper mapper, ICategoryService categoryService)
        {
            _logger = logger;
            _unitOfWorfkMedia = unitOfWorkMedia;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public ICollection<CategoryDto> GetCategories(string rootCategory)
        {
            return _categoryService.LoadDatas(rootCategory, false);
        }

        public bool Create(IntendedDto intendedDto)
        {
            var intended = _mapper.Map<IntendedDto, Intended>(intendedDto);
            var intendedDetails = _mapper.Map<List<IntendedDetailDto>, List<IntendedDetail>>(intendedDto.Details);

            _unitOfWorfkMedia.IntendedRepository.Add(intended);
            _unitOfWorfkMedia.IntendedDetailRepository.AddRange(intendedDetails);

            return _unitOfWorfkMedia.SaveChanges() > 0;
        }
    }
}
