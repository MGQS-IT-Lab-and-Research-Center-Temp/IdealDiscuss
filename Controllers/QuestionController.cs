﻿using AspNetCoreHero.ToastNotification.Abstractions;
using IdealDiscuss.Models.Question;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers;

[Authorize]
public class QuestionController : Controller
{
    private readonly IQuestionService _questionService;
    private readonly ICategoryService _categoryService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly INotyfService _notyf;

    public QuestionController(
        IQuestionService questionService,
        ICategoryService categoryService,
        IHttpContextAccessor httpContextAccessor,
        INotyfService notyf)
    {
        _questionService = questionService;
        _categoryService = categoryService;
        _httpContextAccessor = httpContextAccessor;
        _notyf = notyf;
    }

    public async Task<IActionResult> Index()
    {
        var questions = await _questionService.GetAllQuestion();
        ViewData["Message"] = questions.Message;
        ViewData["Status"] = questions.Status;

        return View(questions.Data);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _categoryService.SelectCategories();
        ViewData["Message"] = "";
        ViewData["Status"] = false;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateQuestionViewModel request)
    {
        var response = await _questionService.Create(request);

        if (response.Status is false)
        {
            _notyf.Error(response.Message);
            return View();
        }

        _notyf.Success(response.Message);

        return RedirectToAction("Index", "Question");
    }

    public async Task<IActionResult> GetQuestionByCategory(string id)
    {
        var response = await _questionService.GetQuestionsByCategoryId(id);
        ViewData["Message"] = response.Message;
        ViewData["Status"] = response.Status;

        return View(response.Data);
    }

    public async Task<IActionResult> GetQuestionDetail(string id)
    {
        var response = await _questionService.GetQuestion(id);
        ViewData["Message"] = response.Message;
        ViewData["Status"] = response.Status;

        return View(response.Data);
    }

    public async Task<IActionResult> Update(string id)
    {
        var response = await _questionService.GetQuestion(id);

        return View(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Update(string id, UpdateQuestionViewModel request)
    {
        var response = await _questionService.Update(id, request);

        if (response.Status is false)
        {
            _notyf.Error(response.Message);

            return RedirectToAction("Index", "Home");
        }

        _notyf.Success(response.Message);

        return RedirectToAction("Index", "Question");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteQuestion([FromRoute] string id)
    {
        var response = await _questionService.Delete(id);

        if (response.Status is false)
        {
            _notyf.Error(response.Message);
            return View();
        }

        _notyf.Success(response.Message);

        return RedirectToAction("Index", "Question");
    }
}
