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
        public List<CategoryDto> LoadDatas(string rootCategoryType, bool isAll)
        {
            var model = _unitOfWorfkMedia.CategoryRepository.Filter(q=>q.Type == rootCategoryType && (q.NotUse != true || isAll == true)).ToList();

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
            _unitOfWorfkMedia.CategoryRepository.Delete(id);
            return _unitOfWorfkMedia.SaveChanges() > 0;
        }
        public bool Edit(CategoryDto categoryDto)
        {
            try
            {
                var entity = _mapper.Map<CategoryDto, Category>(categoryDto);
                _unitOfWorfkMedia.CategoryRepository.Update(entity, UpdateAccessMode.DENY_UPDATE, "CreatedBy", "CreatedDate", "NotUse", "Type");
                return _unitOfWorfkMedia.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public CategoryDto FindId(Guid id)
        {
            var entity = _unitOfWorfkMedia.CategoryRepository.FindById(id);
            return _mapper.Map<Category, CategoryDto>(entity);
        }
        public bool ChangeStatu(Guid categoryId, bool notUse)
        {
            var entity = _unitOfWorfkMedia.CategoryRepository.FindById(categoryId);
            entity.NotUse = !notUse;
            entity.UpdatedDate = DateTime.Now;
            _unitOfWorfkMedia.CategoryRepository.Update(entity, UpdateAccessMode.ALLOW_UPDATE, "NotUse", "UpdatedDate");

            return _unitOfWorfkMedia.SaveChanges() > 0;
        }

        public string GetDatabaseName()
        {
            return _unitOfWorfkMedia.CategoryRepository.GetDatabaseName();
        }
    }
}
