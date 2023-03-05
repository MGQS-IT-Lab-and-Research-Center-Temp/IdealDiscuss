using IdealDiscuss.Dtos.CommentDto;
using IdealDiscuss.Service.Implementations;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }

        // GET: CommentController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: CommentController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

   
        // GET: CommentController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommentController/Edit/5
        [HttpPost]
       
        public IActionResult Edit(int id, UpdateCommentDto updateCommentDto)
        {
            var commentEdit = _commentService.UpdateComment(id, updateCommentDto);
            ViewBag.Message = commentEdit.Message;
            ViewBag.Status = commentEdit.Status;
            return RedirectToAction("Index");
        }
		// POST: CommentController/Delete/5
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
