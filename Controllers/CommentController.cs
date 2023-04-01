using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IdealDiscuss.Models.Comment;
using Org.BouncyCastle.Asn1.Ocsp;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdealDiscuss.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IQuestionService _questionService;

        public CommentController(
            ICommentService commentService, 
            IQuestionService questionService)
        {
            _commentService = commentService;
            _questionService = questionService;
        }

        public IActionResult Index()
        {
            var response = _commentService.GetAllComment();
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Data);
        }

        public IActionResult GetCommentDetail(string id)
        {
            var response = _commentService.GetComment(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Message"] = "";
            ViewData["Status"] = false;

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCommentViewModel request)
        {
            var response = _commentService.CreateComment(request);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View();
        }

        public IActionResult Edit(string id)
        {
            var response = _commentService.GetComment(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Data);
        }

        [HttpPost]
        public IActionResult Edit(string id, UpdateCommentViewModel request)
        {
            var response = _commentService.UpdateComment(id, request);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return RedirectToAction("Index", "Comment");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/delete")]
        public IActionResult DeleteComment([FromRoute] string id)
        {
            var response = _commentService.DeleteComment(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return RedirectToAction("Index", "Comment");
        }
    }
}
