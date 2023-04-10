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
        private readonly INotyfService _notyf;

        public CommentReportController(
            ICommentReportService commentReportService, 
            IFlagService flagService,
            INotyfService notyf)
        {
            _commentReportService = commentReportService;
            _flagService = flagService;
            _notyf = notyf;
        }

       public IActionResult Index()
       {
           var response = _commentReportService.GetAllCommentReport();
			ViewData["Message"] = response.Message;
			ViewData["Status"] = response.Status;
           return View(response.Data);
       }

        public IActionResult CreateCommentReport()
        {
            ViewBag.FlagLists = _flagService.SelectFlags();
            return View();
        }

        [HttpPost]
        public IActionResult CreateCommentReport(CreateCommentReportViewModel request)
        {
            var response = _commentReportService.CreateCommentReport(request);
			if(response.Status is false)
            {
                _notyf.Error(response.Message);
                return View();
            }

            _notyf.Success(response.Message);

             return RedirectToAction("Index", "Comment");
        }

        public IActionResult GetCommentReportDetail(string id)
        {
            var response = _commentReportService.GetCommentReport(id);

			 if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("GetCommentDetail", "Comment");
            }

            return View(response.Data);
        }

        public IActionResult UpdatecommentReport(string id)
        {
            var response = _commentReportService.GetCommentReport(id);
            
            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index","CommentReport");
            }

            var viewModel = new UpdateCommentReportViewModel
            {
                Id = response.Data.Id,
                AdditionalComment = response.Data.AdditionalComment
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateCommentReport(string id, UpdateCommentReportViewModel request)
        {
            var response = _commentReportService.UpdateCommentReport(id, request);
			 if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View(request);
            }

             _notyf.Success(response.Message);

            return RedirectToAction("Index", "CommentReport");
        }

        [HttpPost]
        public IActionResult DeleteCommentReport(string id)
        {
            var response = _commentReportService.DeleteCommentReport(id);
            
            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "CommentReport");
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "CommentReport");
        }
    }
}
