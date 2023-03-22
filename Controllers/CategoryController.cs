using IdealDiscuss.Service.Interface;
using IdealDiscuss.Dtos.CategoryDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace IdealDiscuss.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService; 
        }
        
        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategory();
            ViewData["Message"] = categories.Message;
            ViewData["Status"] = categories.Status;

            return View(categories.Data);
        }
        
        public IActionResult Create()
        {
            ViewData["Message"] = "";
            ViewData["Status"] = false;

            return View();
        }
       
        [HttpPost("category/{id}/delete")]
        public IActionResult DeleteCategory([FromRoute] int id)
        {
            var response = _categoryService.DeleteCategory(id);
            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;
            return RedirectToAction("Index", "Category");
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryDto request)
        {
            var response = _categoryService.CreateCategory(request);

            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;

            return View(response);
        }

        public IActionResult GetCategory(int id)
        {
            var response = _categoryService.GetCategory(id);
            ViewBag.Message = response.Message;
            return View(response.Data);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
		public IActionResult Update(int id, UpdateCategoryDto updateCategoryDto)
		{
			var categoryUpdate = _categoryService.UpdateCategory(id, updateCategoryDto);
			ViewBag.Message = categoryUpdate.Message;
			ViewBag.Status = categoryUpdate.Status;
			return RedirectToAction("Index", "Category");
		}
	}
}
