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

        public async Task<IActionResult> Index()
        {
            var response = await _categoryService.GetAllCategory();
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel request)
        {
            var response = await _categoryService.CreateCategory(request);

			if (response.Status is false)
			{
				_notyf.Error(response.Message);
				return View(request);
			}

			_notyf.Success(response.Message);

			return RedirectToAction("Index", "Category"); ;
		}

        public async Task<IActionResult> GetCategory(string id)
        {
            var response = await _categoryService.GetCategory(id);

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
        public async Task<IActionResult> Update(string id, UpdateCategoryViewModel request)
        {
            var response = await _categoryService.UpdateCategory(id, request);

			if (response.Status is false)
			{
				_notyf.Error(response.Message);
				return View(request);
			}

			_notyf.Success(response.Message);

			return RedirectToAction("Index", "Category");
		}

        [HttpPost]
        public async Task<IActionResult> DeleteCategory([FromRoute] string id)
        {
            var response = await _categoryService.DeleteCategory(id);

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
