using IdealDiscuss.Dtos.QuestionDto;
using IdealDiscuss.Models.Question;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdealDiscuss.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly ICategoryService _categoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<QuestionController> _logger;

        public QuestionController(
            ILogger<QuestionController> logger, 
            IQuestionService questionService, 
            ICategoryService categoryService, 
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _questionService = questionService;
            _categoryService = categoryService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var questions = _questionService.GetAllQuestion();
            ViewData["Message"] = questions.Message;
            ViewData["Status"] = questions.Status;

            return View(questions.Questions);
        }

        public IActionResult Create()
        {
            var category = _categoryService.GetAllCategory();
            ViewData["Categories"] = new SelectList(category.Data, "Id", "Name");
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

        [HttpGet("{id}")]
        public IActionResult GetQuestionByCategory(string id)
        {
            var response = _questionService.GetQuestionsByCategoryId(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Questions);
        }

        public IActionResult GetQuestionDetail(string id)
        {
            var response = _questionService.GetQuestion(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Question);
        }

        public IActionResult Update(string id)
        {
            var response = _questionService.GetQuestion(id);
            return View(response.Question);
        }

        [HttpPost]
        public IActionResult Update(string id, UpdateQuestionViewModel updateQuestionDto)
        {
            var response = _questionService.Update(id, updateQuestionDto);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return RedirectToAction("Index", "Question");
        }

        [HttpPost("{id}/delete")]
        public IActionResult DeleteQuestion([FromRoute] string id)
        {
            var response = _questionService.Delete(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return RedirectToAction("Index", "Question");
        }
    }
}
