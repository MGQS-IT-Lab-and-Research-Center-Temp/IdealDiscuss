using IdealDiscuss.Models.QuestionReport;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers
{
    public class QuestionReportController : Controller
    {
        private readonly IQuestionReportService _questionReportService;

        public QuestionReportController (IQuestionReportService questionReportService)
        {
            _questionReportService = questionReportService;
        }

        public IActionResult Index()
        {
            var response = _questionReportService.GetAllQuestionReport();
            ViewBag.Message = response.Message;
            ViewBag.status = response.Status;

            return View(response.Data);
        }

        public IActionResult ReportQuestion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ReportQuestion(CreateQuestionReportViewModel Report)
        {
            var response = _questionReportService.CreateQuestionReport(Report);
            ViewBag.Message = response.Message;
            ViewBag.status = response.Status;
            return View(response);
        }
        
        public IActionResult GetQuestionReport(string id)
        {
            var response = _questionReportService.GetQuestionReport(id);
            return View (response.Data);
        }

        [HttpGet]
        public IActionResult GetAllQuestionReport()
        {
            var response = _questionReportService.GetAllQuestionReport();
            return View(response.Data);
        }

        public IActionResult UpdateQuestionReport(string id)
        {
            var response = _questionReportService.GetQuestionReport(id);
            return View(response.Data);
        }

        [HttpPost]
        public IActionResult UpdateQuestionReport(string id, UpdateQuestionReportViewModel request)
        {
            var response = _questionReportService.UpdateQuestionReport(id, request);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteQuestionReport(string id)
        {
            var response = _questionReportService.DeleteQuestionReport(id);
            return RedirectToAction("Index", "QuestionReport");
        }
    }
}
