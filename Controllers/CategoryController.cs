using IdealDiscuss.Dtos.CategoryDto;
using IdealDiscuss.Dtos.RoleDto;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger _logger;


        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
        {
            _logger = logger;
           _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryDto request)
        {
            var response = _roleService.CreateCategory(request);

            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;

            return View(response);
        }

    }
}
