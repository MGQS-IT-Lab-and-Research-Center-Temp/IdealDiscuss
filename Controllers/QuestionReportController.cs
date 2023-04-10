using AspNetCoreHero.ToastNotification.Abstractions;
using IdealDiscuss.Models.QuestionReport;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers
{
    public class QuestionReportController : Controller
    {
        private readonly IQuestionReportService _questionReportService;
        private readonly INotyfService _notyf;
        private readonly IFlagService _flagService;

        public QuestionReportController(
            IQuestionReportService questionReportService,
            IFlagService flagService,
            INotyfService notyf)
        {
            _questionReportService = questionReportService;
            _notyf = notyf;
            _flagService = flagService;
        }

        //public IActionResult Index()
        //{
        //    var response = _questionReportService.GetAllQuestionReport();
        //    ViewBag.Message = response.Message;
        //    ViewBag.status = response.Status;

        //    return View(response.Data);
        //}

        public IActionResult ReportQuestion()
        {
            ViewBag.FlagLists = _flagService.SelectFlags();

            return View();
        }

        [HttpPost]
        public IActionResult ReportQuestion(CreateQuestionReportViewModel Report)
        {
            var response = _questionReportService.CreateQuestionReport(Report);

            if (response.Status is false)
            {
                return View(response);
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult GetQuestionReport(string id)
        {
            var response = _questionReportService.GetQuestionReport(id);

            if (response.Status is false)
            {
                return View(response);
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index");
        }

        public IActionResult UpdateQuestionReport(string id)
        {
            var response = _questionReportService.GetQuestionReport(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Question");
            }

            return RedirectToAction("Index", "Question");
        }

        [HttpPost]
        public IActionResult UpdateQuestionReport(string id, UpdateQuestionReportViewModel request)
        {
            var response = _questionReportService.UpdateQuestionReport(id, request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Question");
            }

            _notyf.Success(response.Message);
            return RedirectToAction("Index", "Question");
        }

        [HttpPost]
        public IActionResult DeleteQuestionReport(string id)
        {
            var response = _questionReportService.DeleteQuestionReport(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Question");
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Question");
        }
    }
}
