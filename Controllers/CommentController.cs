<<<<<<< HEAD
﻿using IdealDiscuss.Service.Interface;
=======
﻿using IdealDiscuss.Dtos.CommentDto;
using IdealDiscuss.Service.Interface;
>>>>>>> origin/CommentCreateController
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly ILogger<CommentController> _logger;

<<<<<<< HEAD
        public CommentController(ICommentService commentService,ILogger<CommentController> logger)
=======
        public CommentController(ILogger<CommentController> logger, ICommentService commentService)
        {
            _logger = logger;
            _commentService = commentService;
        }
        //GET: CommentController
        public ActionResult Index()
>>>>>>> origin/CommentCreateController
        {
            _commentService = commentService;
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
<<<<<<< HEAD
        [HttpGet]
=======
>>>>>>> origin/CommentCreateController
        public IActionResult Create()
        {
            return View();
        }

<<<<<<< HEAD
   
        // GET: CommentController/Edit/5
        public IActionResult Edit(int id)
=======
        // POST: CommentController/Create
        [HttpPost]
        public IActionResult Create(CreateCommentDto request)
        {
            var response = _commentService.CreateComment(request);
            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;
            return View(response);
        }

        //GET: CommentController/Edit/5
        public ActionResult Edit(int id)
>>>>>>> origin/CommentCreateController
        {
            return View();
        }

        // POST: CommentController/Edit/5
        [HttpPost]
<<<<<<< HEAD
       
        public IActionResult Edit()
=======
        public ActionResult Edit(int id, IFormCollection collection)
>>>>>>> origin/CommentCreateController
        {
            var response = _commentService.CreateComment(request);
            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;
            return View(response);
        }

<<<<<<< HEAD
        // GET: CommentController/Delete/5
        public IActionResult Delete(int id)
=======
        //GET: CommentController/Delete/5
        public ActionResult Delete(int id)
>>>>>>> origin/CommentCreateController
        {
            return View();
        }

        // POST: CommentController/Edit/5
        [HttpPost]
<<<<<<< HEAD
        public IActionResult Delete()
=======
        public ActionResult Delete(int id, IFormCollection collection)
>>>>>>> origin/CommentCreateController
        {
            var commentUpdate = _commentService.UpdateComment(id, updateCommentDto);
            ViewBag.Message = commentUpdate.Message;
            ViewBag.Status = commentUpdate.Status;
            return RedirectToAction("Index");
        }
    }
}
