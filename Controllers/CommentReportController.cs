﻿using IdealDiscuss.Models.CommentReport;
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

        public IActionResult CreateCommentReport()
        {
            ViewBag.FlagLists = _flagService.SelectFlags();
            ViewData["Message"] = "";
            ViewData["Status"] = false;

            return View();
        }

        [HttpPost]
        public IActionResult CreateCommentReport(CreateCommentReportViewModel request)
        {
            var response = _commentReportService.CreateCommentReport(request);
			ViewData["Message"] = response.Message;
			ViewData["Status"] = response.Status;

			return View();
        }

        public IActionResult Index()
        {
            var response = _commentReportService.GetAllCommentReport();
			ViewData["Message"] = response.Message;
			ViewData["Status"] = response.Status;
            return View(response.Data);
        }

        public IActionResult GetCommentReportDetail(string id)
        {
            var response = _commentReportService.GetCommentReport(id);
			ViewData["Message"] = response.Message;
			ViewData["Status"] = response.Status;

			return View(response.Data);
        }

        public IActionResult UpdatecommentReport(string id)
        {
            var response = _commentReportService.GetCommentReport(id);
            return View(response.Data);
        }

        [HttpPost]
        public IActionResult UpdateCommentReport(string id, UpdateCommentReportViewModel request)
        {
            var response = _commentReportService.UpdateCommentReport(id, request);
			ViewData["Message"] = response.Message;
			ViewData["Status"] = response.Status;
			return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteCommentReport(string id)
        {
            var response = _commentReportService.DeleteCommentReport(id);
			ViewData["Message"] = response.Message;
			ViewData["Status"] = response.Status;

            return RedirectToAction("Index");
        }
    }
}
