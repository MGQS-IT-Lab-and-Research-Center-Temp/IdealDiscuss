using IdealDiscuss.Dtos.FlagDto;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers
{
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
            ViewBag.Message = instances.Message;
            ViewBag.Status = instances.Status;

            return View(instances.Reports);
        }

        public IActionResult CreateFlag()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateFlag(CreateFlagDto request)
        {
            var response = _flagService.CreateFlag(request);

            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;
            return View(response);
        }

        public IActionResult GetFlagDetail(int id)
        {
            var response = _flagService.GetFlag(id);
            return View(response.Report);
        }

        public IActionResult UpdateFlag(int id)
        {
            var response = _flagService.GetFlag(id);
            return View(response.Report);
        }

        [HttpPost]
        public IActionResult UpdateFlag(int id,UpdateFlagDto request)
        {
            var response = _flagService.UpdateFlag(id,request);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteFlag(int id)
        {
            var response = _flagService.DeleteFlag(id);
            return RedirectToAction("Index", "Flag");
        }
    }
}
