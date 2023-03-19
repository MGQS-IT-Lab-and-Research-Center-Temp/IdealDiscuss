using IdealDiscuss.Service.Interface;
using IdealDiscuss.Dtos.CommentDto;
using Microsoft.AspNetCore.Mvc;
using IdealDiscuss.Service.Implementations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace IdealDiscuss.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CommentController> _logger;
        public CommentController(ILogger<CommentController> logger, ICommentService commentService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _commentService = commentService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var comments = _commentService.GetAllComment();
            ViewBag.Message = comments.Message;
            ViewBag.Status = comments.Status;

            return View(comments.Comments);
        }

        public IActionResult GetCommentDetail(int id)
        {
            var response = _commentService.GetComment(id);

            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;

            return View(response.Comment);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCommentDto request)
        {
            var user = _httpContextAccessor.HttpContext.User;
            request.UserId = Convert.ToInt32(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = _commentService.CreateComment(request);
            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;
            return View(response);
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id, UpdateCommentDto updateCommentDto)
        {
            var response = _commentService.UpdateComment(id, updateCommentDto);
            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;
            return RedirectToAction("Index", "Comment");
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("comment/{id}/delete")]
        public IActionResult DeleteComment([FromRoute] int id)
        {
            var response = _commentService.DeleteComment(id);
            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;
            return RedirectToAction("Index", "Comment");
        }
    }
}
