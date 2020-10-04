using AutoMapper;
using DEM.EF;
using DEM.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEM.App
{
    public class CategoryService : ICategoryService
    {
        private ILogger<CategoryService> _logger { get; set; }
        private readonly IUnitOfWorkMedia _unitOfWorfkMedia;
        private readonly IMapper _mapper;
        public CategoryService(ILogger<CategoryService> logger, IUnitOfWorkMedia unitOfWorkMedia, IMapper mapper)
        {
            _logger = logger;
            _unitOfWorfkMedia = unitOfWorkMedia;
            _mapper = mapper;
        }
        public List<CategoryDto> LoadDatas(string rootCategoryType)
        {
            var model = _unitOfWorfkMedia.CategoryRepository.Filter(q=>q.Type == rootCategoryType && q.NotUse != true ).ToList();

            return _mapper.Map<List<Category>, List<CategoryDto>>(model);
        }
        public bool Add(CategoryDto categoryDto)
        {            
            var category = _mapper.Map<Category>(categoryDto);
            _unitOfWorfkMedia.CategoryRepository.Add(category);

            return _unitOfWorfkMedia.SaveChanges() > 0;
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Edit(CategoryDto categoryDto)
        {
            throw new NotImplementedException();
        }

       
    }
}
