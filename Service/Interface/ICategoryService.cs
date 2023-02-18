using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.CategoryDto;

namespace IdealDiscuss.Service.Interface
{
     public interface ICategoryService
     {
          BaseResponseModel CreateCategory(CreateCategoryDto createCategoryDto);
          BaseResponseModel DeleteCategory(int categoryId);
          BaseResponseModel UpdateCategory(int categoryId, UpdateCategoryDto updateCategoryDto);
          CategoryResponseModel GetCategory(int categoryId);
          CategoriesResponseModel GetAllCategory();
     }
}
