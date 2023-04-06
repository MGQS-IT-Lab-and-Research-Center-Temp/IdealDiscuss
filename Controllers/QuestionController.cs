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

    public QuestionController(
        IQuestionService questionService,
        ICategoryService categoryService,
        IHttpContextAccessor httpContextAccessor)
    {
        _questionService = questionService;
        _categoryService = categoryService;
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Index()
    {
        var questions = _questionService.GetAllQuestion();
        ViewData["Message"] = questions.Message;
        ViewData["Status"] = questions.Status;

        return View(questions.Data);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = _categoryService.SelectCategories();
        ViewData["Message"] = "";
        ViewData["Status"] = false;

        return View();
    }

    [HttpPost]
    public IActionResult Create(CreateQuestionViewModel request)
    {
        var response = _questionService.Create(request);
        ViewData["Message"] = response.Message;
        ViewData["Status"] = response.Status;

        return View();
    }

    public IActionResult GetQuestionByCategory(string id)
    {
        var response = _questionService.GetQuestionsByCategoryId(id);
        ViewData["Message"] = response.Message;
        ViewData["Status"] = response.Status;

        return View(response.Data);
    }

    public IActionResult GetQuestionDetail(string id)
    {
        var response = _questionService.GetQuestion(id);
        ViewData["Message"] = response.Message;
        ViewData["Status"] = response.Status;

        return View(response.Data);
    }

    public IActionResult Update(string id)
    {
        var response = _questionService.GetQuestion(id);
        return View(response.Data);
    }

    [HttpPost]
    public IActionResult Update(string id, UpdateQuestionViewModel request)
    {
        var response = _questionService.Update(id, request);
        ViewData["Message"] = response.Message;
        ViewData["Status"] = response.Status;

        return RedirectToAction("Index", "Question");
    }

    [HttpPost]
    public IActionResult DeleteQuestion([FromRoute] string id)
    {
        var response = _questionService.Delete(id);
        ViewData["Message"] = response.Message;
        ViewData["Status"] = response.Status;

        return RedirectToAction("Index", "Question");
    }
}
