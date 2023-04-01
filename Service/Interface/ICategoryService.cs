using IdealDiscuss.Models;
using IdealDiscuss.Models.Category;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdealDiscuss.Service.Interface
{
    public interface ICategoryService
    {
        BaseResponseModel CreateCategory(CreateCategoryViewModel createCategoryDto);
        BaseResponseModel DeleteCategory(string categoryId);
        BaseResponseModel UpdateCategory(string categoryId, UpdateCategoryViewModel updateCategoryDto);
        CategoryResponseModel GetCategory(string categoryId);
        CategoriesResponseModel GetAllCategory();
        IEnumerable<SelectListItem> SelectCategories();
    }
}
