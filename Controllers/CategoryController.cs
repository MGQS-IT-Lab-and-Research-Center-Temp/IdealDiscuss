using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IdealDiscuss.Models.Category;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace IdealDiscuss.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
		private readonly INotyfService _notyf;

		public CategoryController(ICategoryService categoryService, INotyfService notyfService)
        {
            _categoryService = categoryService;
            _notyf = notyfService;
        }

        public IActionResult Index()
        {
            var response = _categoryService.GetAllCategory();
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryViewModel request)
        {
            var response = _categoryService.CreateCategory(request);

			if (response.Status is false)
			{
				_notyf.Error(response.Message);
				return View(request);
			}

			_notyf.Success(response.Message);

			return RedirectToAction("Index", "Category"); ;
		}

        public IActionResult GetCategory(string id)
        {
            var response = _categoryService.GetCategory(id);

			if (response.Status is false)
			{
				_notyf.Error(response.Message);
                return RedirectToAction("Index", "Category");
            }

            _notyf.Success(response.Message);

            return View(response.Data);

        }

        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(string id, UpdateCategoryViewModel request)
        {
            var response = _categoryService.UpdateCategory(id, request);

			if (response.Status is false)
			{
				_notyf.Error(response.Message);
				return View(request);
			}

			_notyf.Success(response.Message);

			return RedirectToAction("Index", "Category");
		}

        [HttpPost("{id}/delete")]
        public IActionResult DeleteCategory([FromRoute] string id)
        {
            var response = _categoryService.DeleteCategory(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Category");
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Category");
        }
    }
}
