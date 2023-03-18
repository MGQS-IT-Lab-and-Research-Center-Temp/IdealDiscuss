using IdealDiscuss.Dtos.QuestionDto;
using IdealDiscuss.Entities;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Claims;

namespace IdealDiscuss.Controllers
{
    public class QuestionController : Controller
    {

        private readonly IQuestionService _questionService;
        private readonly ICategoryService _categoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<QuestionController> _logger;

        public QuestionController(ILogger<QuestionController> logger, IQuestionService questionService,ICategoryService categoryService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _questionService = questionService;
            _categoryService = categoryService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var questions = _questionService.GetAllQuestion();
            ViewBag.Message = questions.Message;
            ViewBag.Status = questions.Status;

            return View(questions.questions);
        }

        public IActionResult Create()
        {
           var category = _categoryService.GetAllCategory();
            ViewBag.Categories = new SelectList(category.Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateQuestionDto request)
        {
            var response = _questionService.Create(request);
            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;
            return RedirectToAction("Index");
        }

        [HttpGet("getquestionbycategory/{id}")]
        public IActionResult GetQuestionByCategory(int id)
        {
            var response = _questionService.GetQuestionsByCategoryId(id);
            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;

            return View(response.questions);
        }
        
        public IActionResult GetQuestionDetail(int id)
        {
            var response = _questionService.GetQuestion(id);
            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;

            return View(response.question);
        }

        public IActionResult Update(int id)
        {
            var response = _questionService.GetQuestion(id);
            return View(response.question);
        }

        [HttpPost]
        public IActionResult Update(int id, UpdateQuestionDto updateQuestionDto)
        {
            var questionUpdate = _questionService.Update(id, updateQuestionDto);
            ViewBag.Message = questionUpdate.Message;
            ViewBag.Status = questionUpdate.Status;
            return RedirectToAction("Index", "Question");
        }

        [HttpPost("question/{id}/delete")]
        public IActionResult DeleteQuestion([FromRoute] int id)
        {
            var response = _questionService.Delete(id);
            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;
            return RedirectToAction("Index", "Question");
        }


    }
}
