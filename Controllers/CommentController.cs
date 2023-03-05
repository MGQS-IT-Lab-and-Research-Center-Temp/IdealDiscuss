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
        //GET: CommentController
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

        [HttpPost]
        public ActionResult Edit(int id,UpdateCommentDto updateCommentDto)
        {
            var response = _commentService.UpdateComment(id,updateCommentDto);
            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;
            return View(response);
        }

        // POST: CommentController/Edit/5

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
