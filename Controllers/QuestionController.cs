using IdealDiscuss.Dtos.QuestionDto;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers
{
    public class QuestionController : Controller
    {

        private readonly IQuestionService _questionService;
        private readonly ILogger<QuestionController> _logger;

        public QuestionController(ILogger<QuestionController> logger, IQuestionService questionService)
        {
            _logger = logger;
            _questionService = questionService;
        }

        public IActionResult Index()
        {
            var questions = _questionService.GetAllQuestion();
            ViewBag.Message = questions.Message;
            ViewBag.Status = questions.Status;

            return View(questions.Reports);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateQuestionDto request)
        {
            var response = _questionService.Create(request);

            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;

            return View(response);
        }

        public IActionResult GetQuestionDetail(int id)
        {
            var response = _questionService.GetQuestion(id);
            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;

            return View(response.Report);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var response = _questionService.GetQuestion(id);
            return View(response.Report);
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
