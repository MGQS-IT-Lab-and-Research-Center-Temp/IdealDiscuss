﻿using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly ILogger<CommentController> _logger;


        public CommentController(ICommentService commentService, IUserService userService, ILogger<CommentController> logger)
        {
            _commentService = commentService;
            _userService = userService;
            _logger = logger;
        }
        // GET: CommentController
        public IActionResult Index()
        {
            var comments = _commentService.GetAllComment();
            return View(comments.Comments);
        }

        // GET: CommentController/Details/5
        public IActionResult GetCommentDetail(int id)
        {
            var response = _commentService.GetComment(id);

            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;

            return View(response.Comment);
        }

        // GET: CommentController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

   
        // GET: CommentController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommentController/Edit/5
        [HttpPost]
       
        public IActionResult Edit()
        {
            return View();
        }

        // GET: CommentController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommentController/Delete/5
        [HttpPost]
        public IActionResult Delete()
        {
            return View();
        }
    }
}
