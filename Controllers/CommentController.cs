using IdealDiscuss.Service.Interface;
using IdealDiscuss.Dtos.CommentDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace IdealDiscuss.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(
            ICommentService commentService,
            IHttpContextAccessor httpContextAccessor)
        {
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            var response = _commentService.GetAllComment();
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Comments);
        }

        public IActionResult GetCommentDetail(int id)
        {
            var response = _commentService.GetComment(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Comment);
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

        public IActionResult Edit(int id)
        {
            var response = _commentService.GetComment(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Comment);
        }

        [HttpPost]
        public IActionResult Edit(int id, UpdateCommentDto request)
        {
            var response = _commentService.UpdateComment(id, request);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return RedirectToAction("Index", "Comment");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/delete")]
        public IActionResult DeleteComment([FromRoute] int id)
        {
            var response = _commentService.DeleteComment(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return RedirectToAction("Index", "Comment");
        }
    }
}
