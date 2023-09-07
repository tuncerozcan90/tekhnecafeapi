using AutoMapper;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.DTOs.Category;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IProductDal _productDal;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryDal categoryDal, IMapper mapper, IProductDal productDal)
        {
            _categoryDal = categoryDal;
            _mapper = mapper;
            _productDal = productDal;
        }

        public async Task CreateCategoryAsync(CategoryAddDto categoryAddDto)
        {
            Category category = _mapper.Map<Category>(categoryAddDto);
            await _categoryDal.AddAsync(category);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            Category category = await _categoryDal.GetByIdAsync(Guid.Parse(id));
            var products = _productDal.GetProductsByCategory(id);
            foreach (var product in products)
            {
                product.IsDeleted = true;
                await _productDal.UpdateAsync(product);
            }
            category.IsDeleted = true;

            await _categoryDal.SafeDeleteAsync(category);
        }

        public List<CategoryListDto> GetAllCategory()
        {
            var category = _categoryDal.GetAll(_ => !_.IsDeleted);
            return _mapper.Map<List<CategoryListDto>>(category);
        }

        public async Task UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto)
        {
            Category category = await _categoryDal.GetByIdAsync(Guid.Parse(categoryUpdateDto.Id));
            _mapper.Map(categoryUpdateDto, category);
            await _categoryDal.UpdateAsync(category);
        }
    }
}
