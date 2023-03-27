using IdealDiscuss.Service.Interface;
using IdealDiscuss.Dtos.CommentDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace IdealDiscuss.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
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
        public IActionResult Create(CreateCommentDto request)
        {
            var response = _commentService.CreateComment(request);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response);
        }

        public IActionResult Edit(string id)
        {
            var response = _commentService.GetComment(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Data);
        }

        [HttpPost]
        public IActionResult Edit(string id, UpdateCommentDto request)
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
