using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers
{
    public class CategoryController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

    }
}
