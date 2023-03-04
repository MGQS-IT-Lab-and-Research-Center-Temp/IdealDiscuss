using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
        {
            _categoryService = categoryService; 
            _logger = logger;   
        }
        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategory();
            return View(categories.Data);
        }
    }
}
