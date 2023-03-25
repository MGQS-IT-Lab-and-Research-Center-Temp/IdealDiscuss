using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.CategoryDto;

namespace IdealDiscuss.Service.Interface
{
     public interface ICategoryService
     {
          BaseResponseModel CreateCategory(CreateCategoryDto createCategoryDto);
          BaseResponseModel DeleteCategory(string categoryId);
          BaseResponseModel UpdateCategory(string categoryId, UpdateCategoryDto updateCategoryDto);
          CategoryResponseModel GetCategory(string categoryId);
          CategoriesResponseModel GetAllCategory();
     }
}
