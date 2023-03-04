using IdealDiscuss.Service.Implementations;
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
            _logger = logger;
            _categoryService = categoryService;
         }
            [HttpPost]
            public IActionResult GetCategory(int id)
            {
                var response = _categoryService.GetCategory(id);
                ViewBag.Message = response.Message;
                return View(response.Data);
            }

    }
}
