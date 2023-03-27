using IdealDiscuss.Dtos.FlagDto;
using IdealDiscuss.Dtos.QuestionReportDto;
using IdealDiscuss.Entities;
using IdealDiscuss.Service.Implementations;
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

            return View(response.Reports);
        }

        public IActionResult ReportQuestion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ReportQuestion(CreateQuestionReportDto Report)
        {
            var response = _questionReportService.CreateQuestionReport(Report);
            ViewBag.Message = response.Message;
            ViewBag.status = response.Status;
            return View(response);
        }
        
        public IActionResult GetQuestionReport(string id)
        {
            var response = _questionReportService.GetQuestionReport(id);
            return View (response.Report);
        }

        [HttpGet]
        public IActionResult GetAllQuestionReport()
        {
            var response = _questionReportService.GetAllQuestionReport();
            return View(response.Reports);
        }

        public IActionResult UpdateQuestionReport(string id)
        {
            var response = _questionReportService.GetQuestionReport(id);
            return View(response.Report);
        }

        [HttpPost]
        public IActionResult UpdateQuestionReport(string id, UpdateQuestionReportDto request)
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
