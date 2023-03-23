﻿using IdealDiscuss.Dtos.CommentReport;
using IdealDiscuss.Service.Implementations;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace IdealDiscuss.Controllers
{
    public class CommentReportController : Controller
    {
        private readonly ICommentReportService _commentReportService;
        private readonly IFlagService _flagService;
        public CommentReportController(ICommentReportService commentReportService, IFlagService flagService)
        {
            _commentReportService = commentReportService;
            _flagService = flagService;
        }
        public IActionResult CreateCommentReport()
        {
            var flags = _flagService.GetAllFlag();
            ViewBag.flagLists = flags;
            return View();
        }
        public IActionResult CreateCommentReport(CreateCommentReportDto createCommentReportDto)
        {
            var response = _commentReportService.CreateCommentReport(createCommentReportDto);
			ViewData["Message"] = response.Message;
			ViewData["Status"] = response.Status;
			return View(response);
        }


        public IActionResult Index()
        {
            var response = _commentReportService.GetAllCommentReport();
			ViewData["Message"] = response.Message;
			ViewData["Status"] = response.Status;
            return View(response.CommentReports);
        }


        public IActionResult GetCommentReportDetail(int id)
        {
            var response = _commentReportService.GetCommentReport(id);
			ViewData["Message"] = response.Message;
			ViewData["Status"] = response.Status;
			return View(response.CommentReport);
        }


        public IActionResult UpdatecommentReport(int id)
        {
            var response = _commentReportService.GetCommentReport(id);
            return View(response.CommentReport);
        }


        [HttpPost]
        public IActionResult UpdateCommentReport(int id,UpdateCommentReportDto updateCommentReportDto)
        {
            var response = _commentReportService.UpdateCommentReport(id,updateCommentReportDto);
			ViewData["Message"] = response.Message;
			ViewData["Status"] = response.Status;
			return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteCommentReport(int id)
        {
            var response = _commentReportService.DeleteCommentReport(id);
			ViewData["Message"] = response.Message;
			ViewData["Status"] = response.Status;

            return RedirectToAction("Index");
        }

    }
}