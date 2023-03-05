using IdealDiscuss.Dtos.CommentDto;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult Index()
        {
            return View();
        }

        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult Edit(int id, IFormCollection collection)
        {
            return View();
        }

        //GET: CommentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommentController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            return View();
        }
    }
}
