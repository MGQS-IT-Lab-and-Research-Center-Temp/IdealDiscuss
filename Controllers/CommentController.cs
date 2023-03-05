using IdealDiscuss.Dtos.CommentDto;
using IdealDiscuss.Dtos.RoleDto;
using IdealDiscuss.Service.Implementations;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ICommentService commentService,ILogger<CommentController> logger)
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
        public IActionResult Create()
        {
            return View();
        }

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
        {
            return View();
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UpdateCommentDto updateCommentDto)
        {
            var commentUpdate = _commentService.UpdateComment(id, updateCommentDto);
            ViewBag.Message = commentUpdate.Message;
            ViewBag.Status = commentUpdate.Status;
            return RedirectToAction("Index");
        }

        [HttpPost("comment/{id}/delete")]
        public IActionResult DeleteComment([FromRoute] int id)
        {
            var response = _commentService.DeleteComment(id);
            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;
            return RedirectToAction("Index", "Role");
        }
    }
}
