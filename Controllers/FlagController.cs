using IdealDiscuss.Dtos.FlagDto;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FlagController : Controller
    {
        private readonly IFlagService _flagService;

        public FlagController(IFlagService flagService)
        {
            _flagService = flagService;
        }

        public IActionResult Index()
        {
            var instances = _flagService.GetAllFlag();

            ViewData["Message"] = instances.Message;
            ViewData["Status"] = instances.Status;

            return View(instances.Data);
        }

        public IActionResult CreateFlag()
        {
            ViewData["Message"] = "";
            ViewData["Status"] = false;

            return View();
        }

        [HttpPost]
        public IActionResult CreateFlag(CreateFlagDto request)
        {
            var response = _flagService.CreateFlag(request);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response);
        }

        public IActionResult GetFlagDetail(string id)
        {
            var response = _flagService.GetFlag(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;
            ViewData["ControllerName"] = "Flag";
            ViewData["ActionName"] = "Index";

            return View(response.Data);
        }

        public IActionResult UpdateFlag(string id)
        {
            var response = _flagService.GetFlag(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;
            ViewData["ControllerName"] = "Flag";
            ViewData["ActionName"] = "Index";

            return View(response.Data);
        }

        [HttpPost]
        public IActionResult UpdateFlag(string id,UpdateFlagDto request)
        {
            var response = _flagService.UpdateFlag(id,request);
            return RedirectToAction("Index", "Flag");
        }

        [HttpPost]
        public IActionResult DeleteFlag(string id)
        {
            var response = _flagService.DeleteFlag(id);
            return RedirectToAction("Index", "Flag");
        }
    }
}
