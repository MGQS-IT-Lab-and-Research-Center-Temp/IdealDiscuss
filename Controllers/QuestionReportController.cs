using AspNetCoreHero.ToastNotification.Abstractions;
using IdealDiscuss.Models.QuestionReport;
using IdealDiscuss.Service.Implementations;
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

        //    return RedirectToAction("Index", "QuestionReport");
        //}

        public async Task<IActionResult> ReportQuestion()
        {
            ViewBag.FlagLists = await _flagService.SelectFlags();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReportQuestion(CreateQuestionReportViewModel Report)
        {
            var response = await _questionReportService.CreateQuestionReport(Report);

            if (response.Status is false)
            {
                return View(response);
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> GetQuestionReport(string id)
        {
            var response = await _questionReportService.GetQuestionReport(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);

                return RedirectToAction("Index", "Question");
            }

            return View(response.Data);
        }

        public async Task<IActionResult> GetQuestionReports(string id)
        {
            var response = await _questionReportService.GetQuestionReports(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Question");
            }

            return View(response.Data);
        }

        public async Task<IActionResult> UpdateQuestionReport(string id)
        {
            var response = await _questionReportService.GetQuestionReport(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Question");
            }

            return RedirectToAction("Index", "Question");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuestionReport(string id, UpdateQuestionReportViewModel request)
        {
            var response = await _questionReportService.UpdateQuestionReport(id, request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Question");
            }

            _notyf.Success(response.Message);
            return RedirectToAction("Index", "Question");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteQuestionReport(string id)
        {
            var response = await _questionReportService.DeleteQuestionReport(id);

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
