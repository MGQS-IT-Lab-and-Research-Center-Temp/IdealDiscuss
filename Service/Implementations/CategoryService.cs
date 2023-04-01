using IdealDiscuss.Entities;
using IdealDiscuss.Models;
using IdealDiscuss.Models.Category;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace IdealDiscuss.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public BaseResponseModel CreateCategory(CreateCategoryViewModel request)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;

            var isCategoryExist = _unitOfWork.Categories.Exists(c => c.Name == request.Name);

            if (isCategoryExist)
            {
                response.Message = "Category already exist!";
                return response;
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                response.Message = "Category name is required!";
                return response;
            }

            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                CreatedBy = createdBy,
                DateCreated = DateTime.Now
            };

            try
            {
                _unitOfWork.Categories.Create(category);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Category created successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create category at this time: {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel DeleteCategory(string categoryId)
        {
            var response = new BaseResponseModel();
            var isCategoryExist = _unitOfWork.Categories.Exists(c => c.Id == categoryId && !c.IsDeleted);

            if (!isCategoryExist)
            {
                response.Message = "Category does not exist.";
                return response;
            }

            var category = _unitOfWork.Categories.Get(categoryId);
            category.IsDeleted = true;

            try
            {
                _unitOfWork.Categories.Update(category);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Category successfully deleted.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Can not delete category: {ex.Message}";
                return response;
            }
        }

        public CategoriesResponseModel GetAllCategory()
        {
            var response = new CategoriesResponseModel();

            try
            {
                Expression<Func<Category, bool>> expression = c => c.IsDeleted == false;
                var category = _unitOfWork.Categories.GetAll(expression);

                if (category is null || category.Count == 0)
                {
                    response.Message = "No categories found!";
                    return response;
                }

                response.Data = category.Select(
                    category => new CategoryViewModel
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Description = category.Description
                    }).ToList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }

            response.Status = true;
            response.Message = "Success";

            return response;
        }

        public CategoryResponseModel GetCategory(string categoryId)
        {
            var response = new CategoryResponseModel();

            Expression<Func<Category, bool>> expression = c =>
                                                (c.Id == categoryId)
                                                && (c.Id == categoryId
                                                && c.IsDeleted == false);

            var categoryExist = _unitOfWork.Categories.Exists(expression);

            if (!categoryExist)
            {
                response.Message = $"Category with id {categoryId} does not exist.";
                return response;
            }

            var category = _unitOfWork.Categories.Get(categoryId);

            response.Message = "Success";
            response.Status = true;
            response.Data = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };

            return response;
        }

        public BaseResponseModel UpdateCategory(string categoryId, UpdateCategoryViewModel updateCategoryDto)
        {
            var response = new BaseResponseModel();
            string modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var categoryExist = _unitOfWork.Categories.Exists(c => c.Id == categoryId);

            if (!categoryExist)
            {
                response.Message = "Category does not exist.";
                return response;
            }

            var category = _unitOfWork.Categories.Get(categoryId);
            category.Description = updateCategoryDto.Description;
            category.ModifiedBy = modifiedBy;
            category.LastModified = DateTime.Now;
            try
            {
                _unitOfWork.Categories.Update(category);
                _unitOfWork.SaveChanges();
                response.Message = "Category updated successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the category: {ex.Message}";
                return response;
            }
        }

        public IEnumerable<SelectListItem> SelectCategories()
        {
            return _unitOfWork.Categories.SelectAll().Select(cat => new SelectListItem()
            {
                Text = cat.Name,
                Value = cat.Id
            });
        }
    }
}
