using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
       
        public ActionResult Index()
        {
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

    }
}
