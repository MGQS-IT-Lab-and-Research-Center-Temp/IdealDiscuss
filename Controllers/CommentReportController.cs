using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdealDiscuss.Controllers
{
    [Route("[controller]")]
    public class CommentReportController : Controller
    {
        private readonly ILogger<CommentReportController> _logger;
         private readonly ICommentReportService _commentReportService;
        private readonly IHttpContextAccessor _httpContextAccessor;
         public readonly IFlagService _flagservice;
        public CommentReportController(ILogger<CommentReportController> logger,
                                       ICommentReportService commentReportService,
                                       IFlagService flagservice,
                                       IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _commentReportService = commentReportService;
            _flagservice = flagservice;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult CreatecommentReport()
        {
            var flagList = _flagservice.GetAllFlag();
            ViewBag.flagLists = flagList;
            return View();
        }

        [HttpPost]
        public IActionResult CreatecommentReport(CreateCommentReportDto create)
        {
            var response = _commentReportService.CreateCommentReport(create);
            create.UserId = Convert.ToInt32(_httpContextAccessor.Claims(NameIdentifier));
            return RedirectToAction("Index" "Comment");
        }

        [HttpGet]
        public IActionResult UpdateCommentReport()
        {
             var flagList = _flagservice.GetAllFlag();
            ViewBag.flagLists = flagList;
            return View();
        }

        [HttpPost]
        public IActionResult UpdateCommentReport(int commentReportId,UpdateCommentReportDto update)
        {
            var response = _commentReportService.UpdateCommentReport(commentReportId,update);
            return RedirectToAction("Index" "Comment");
        }

        [HttpGet]
        public IActionResult DeleteCommentReport(int id)
        {
        var response = _commentReportRepository.DeleteCommentReport(id);
        return RedirectToAction("Index" "Comment");
        }

        public IActionResult Index()
        {
            var response = _commentReportRepository.GetAllCommentReport();
            return View(response);
        }

        public GetCommentReport(int id)
        {
            var response = _commentReportService.GetCommentReport(id);
            return View(response);
        }
    }
}