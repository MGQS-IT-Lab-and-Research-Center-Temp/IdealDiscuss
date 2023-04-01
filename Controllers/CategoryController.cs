﻿using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IdealDiscuss.Models.Category;

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
       
        [HttpPost("{id}/delete")]
        public IActionResult DeleteCategory([FromRoute] string id)
        {
            var response = _categoryService.DeleteCategory(id);
            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;
            return RedirectToAction("Index", "Category");
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryViewModel request)
        {
            var response = _categoryService.CreateCategory(request);

            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;

            return View(response);
        }

        public IActionResult GetCategory(string id)
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
		public IActionResult Update(string id, UpdateCategoryViewModel updateCategoryDto)
		{
			var categoryUpdate = _categoryService.UpdateCategory(id, updateCategoryDto);
			ViewBag.Message = categoryUpdate.Message;
			ViewBag.Status = categoryUpdate.Status;
			return RedirectToAction("Index", "Category");
		}
	}
}
