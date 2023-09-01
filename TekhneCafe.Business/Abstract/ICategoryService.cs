using TekhneCafe.Core.DTOs.Category;

namespace TekhneCafe.Business.Abstract
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CategoryAddDto categoryAddDto);
        List<CategoryListDto> GetAllCategory(CategoryListDto categoryListDto);
        Task UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto);
        Task DeleteCategoryAsync(string id);

    }
}
