using IdealDiscuss.Models;
using IdealDiscuss.Models.Category;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdealDiscuss.Service.Interface;

public interface ICategoryService
{
    Task<BaseResponseModel> CreateCategory(CreateCategoryViewModel createCategoryDto);
    Task<BaseResponseModel> DeleteCategory(string categoryId);
    Task<BaseResponseModel> UpdateCategory(string categoryId, UpdateCategoryViewModel updateCategoryDto);
    Task<CategoryResponseModel> GetCategory(string categoryId);
    Task<CategoriesResponseModel> GetAllCategory();
    Task<IReadOnlyList<SelectListItem>> SelectCategories();
}
