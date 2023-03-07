using IdealDiscuss.Service.Interface;
using IdealDiscuss.Dtos.CommentDto;
using Microsoft.AspNetCore.Mvc;
using IdealDiscuss.Service.Implementations;

namespace IdealDiscuss.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly ILogger<CommentController> _logger;
        public CommentController(ILogger<CommentController> logger, ICommentService commentService)
        {
            _logger = logger;
            _commentService = commentService;
        }
       
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
            return View(response);
        }

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
