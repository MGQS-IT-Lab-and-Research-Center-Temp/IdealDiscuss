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
        // GET: CommentController
        // public ActionResult Index()
        // {
        //     return View();
        // }

        // // GET: CommentController/Details/5
        // public ActionResult Details(int id)
        // {
        //     return View();
        // }

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

        // GET: CommentController/Edit/5
        // public ActionResult Edit(int id)
        // {
        //     return View();
        // }

        // // POST: CommentController/Edit/5
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public ActionResult Edit(int id, IFormCollection collection)
        // {
        //     try
        //     {
        //         return RedirectToAction(nameof(Index));
        //     }
        //     catch
        //     {
        //         return View();
        //     }
        // }

        // // GET: CommentController/Delete/5
        // public ActionResult Delete(int id)
        // {
        //     return View();
        // }

        // // POST: CommentController/Delete/5
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public ActionResult Delete(int id, IFormCollection collection)
        // {
        //     try
        //     {
        //         return RedirectToAction(nameof(Index));
        //     }
        //     catch
        //     {
        //         return View();
        //     }
        // }
    }
}
