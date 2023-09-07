using TekhneCafe.Core.DTOs.Category;

namespace TekhneCafe.Business.Abstract
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CategoryAddDto categoryAddDto);
        List<CategoryListDto> GetAllCategory();
        Task DeleteCategoryAsync(string id);
        Task UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto);
    }
}
