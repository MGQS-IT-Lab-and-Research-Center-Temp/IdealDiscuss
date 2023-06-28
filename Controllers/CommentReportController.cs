using IdealDiscuss.Models.CommentReport;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdealDiscuss.Controllers
{
    [Authorize]
    public class CommentReportController : Controller
    {
        private readonly ICommentReportService _commentReportService;
        private readonly IFlagService _flagService;

        public CommentReportController(
            ICommentReportService commentReportService, 
            IFlagService flagService)
        {
            _commentReportService = commentReportService;
            _flagService = flagService;
        }

   //     public IActionResult Index()
   //     {
   //         var response = _commentReportService.GetAllCommentReport();
			//ViewData["Message"] = response.Message;
			//ViewData["Status"] = response.Status;
   //         return View(response.Data);
   //     }

        public async Task<IActionResult> CreateCommentReport()
        {
            ViewBag.FlagLists = await _flagService.SelectFlags();
            ViewData["Message"] = "";
            ViewData["Status"] = false;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommentReport(CreateCommentReportViewModel request)
        {
            var response = await _commentReportService.CreateCommentReport(request);
			ViewData["Message"] = response.Message;
			ViewData["Status"] = response.Status;

			return View();
        }

        public async Task<IActionResult> GetCommentReportDetail(string id)
        {
            var response = await _commentReportService.GetCommentReport(id);
			ViewData["Message"] = response.Message;
			ViewData["Status"] = response.Status;

			return View(response.Data);
        }

        public async Task<IActionResult> UpdatecommentReport(string id)
        {
            var response = await _commentReportService.GetCommentReport(id);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCommentReport(string id, UpdateCommentReportViewModel request)
        {
            var response = await _commentReportService.UpdateCommentReport(id, request);
			ViewData["Message"] = response.Message;
			ViewData["Status"] = response.Status;
			return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCommentReport(string id)
        {
            var response = await _commentReportService.DeleteCommentReport(id);
			ViewData["Message"] = response.Message;
			ViewData["Status"] = response.Status;

            return RedirectToAction("Index");
        }
    }
}
