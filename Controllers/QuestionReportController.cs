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
        private readonly IFlagService  _flagService;

        public QuestionReportController(IQuestionReportService questionReportService, IFlagService flagService)
        {
            _flagService = flagService;
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
            var flags = _flagService.GetAllFlag();
            ViewBag.flagLists = flags;
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
        public IActionResult GetQuestionReport(int id)
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
        public IActionResult UpdateQuestionReport(int id)
        {
            var response = _questionReportService.GetQuestionReport(id);
            return View(response.Report);
        }
        [HttpPost]
        public IActionResult UpdateQuestionReport(int id, UpdateQuestionReportDto request)
        {
            var response = _questionReportService.UpdateQuestionReport(id, request);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteQuestionReport(int id)
        {
            var response = _questionReportService.DeleteQuestionReport(id);
            return RedirectToAction("Index", "QuestionReport");
        }
    }
}
