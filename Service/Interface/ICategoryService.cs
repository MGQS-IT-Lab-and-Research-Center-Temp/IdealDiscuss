using IdealDiscuss.Dtos.CategoryDto;
using IdealDiscuss.Entities;

namespace IdealDiscuss.Service.Interface
{
    public interface ICategoryService
    {
        void CreateCategory(CreateCategoryDto request);
        void UpdateCategory(int id, UpdateCategoryDto updateCategoryDto);
        void ViewCategory(int id, Category category);
    }
}
