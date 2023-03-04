using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers
{
    public class CommentController : Controller
    {
        // GET: CommentController
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
       
        public IActionResult Edit()
        {
            return View();
        }

        // GET: CommentController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommentController/Delete/5
        [HttpPost]
        public IActionResult Delete()
        {
            return View();
        }
    }
}
