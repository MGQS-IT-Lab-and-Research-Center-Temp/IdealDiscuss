using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Http;
﻿using IdealDiscuss.Dtos.CategoryDto
using IdealDiscuss.Service.Interface;
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

        public IActionResult Index()
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
            var response = _categoryService.CreateCategory(request);

            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;

            return View(response);
        }
    }
}
