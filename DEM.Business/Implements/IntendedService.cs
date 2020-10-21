using AutoMapper;
using DEM.EF;
using DEM.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq; 

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

        public Tuple<ICollection<IntendedDto>, int> LoadData(string rootCategory, DateTime startTime, DateTime endTime, int page, int pageSize)
        {
            var query = from intended in _unitOfWorfkMedia.Intendeds
                         where intended.RootCategory == rootCategory
                                && (intended.FromDate <= endTime && intended.ToDate >= startTime)
                            orderby intended.FromDate descending
                         select new IntendedDto()
                         {
                             Id = intended.Id,
                             FromDate = intended.FromDate,
                             ToDate = intended.ToDate,
                             Description = intended.Description
                         };
            var model = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var total = query.Count();

            return new Tuple<ICollection<IntendedDto>, int>(model, total);
        }

        public IntendedDto GetData(Guid intendedId)
        {
            var intended = _unitOfWorfkMedia.IntendedRepository.FindById(intendedId);
            var intendedDto = _mapper.Map<Intended, IntendedDto>(intended);

            var intendedDetail = _unitOfWorfkMedia.IntendedDetailRepository.Filter(o => o.IntendedId == intendedId).ToList();
            intendedDto.Details = _mapper.Map<List<IntendedDetail>, List<IntendedDetailDto>>(intendedDetail);

            return intendedDto;
        }
    }
}
