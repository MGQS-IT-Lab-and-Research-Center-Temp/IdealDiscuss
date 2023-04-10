using AspNetCoreHero.ToastNotification.Abstractions;
using IdealDiscuss.Models.Flag;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FlagController : Controller
    {
        private readonly IFlagService _flagService;
        private readonly INotyfService _notyf;

        public FlagController(IFlagService flagService, INotyfService notyf)
        {
            _flagService = flagService;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            var response = _flagService.GetAllFlag();

            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Data);
        }

        public IActionResult CreateFlag()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateFlag(CreateFlagViewModel request)
        {
            var response = _flagService.CreateFlag(request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View();
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Flag");
        }

        public IActionResult GetFlagDetail(string id)
        {
            var response = _flagService.GetFlag(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Flag");
            }

            return View(response.Data);
        }

        public IActionResult Update(string id)
        {
            var response = _flagService.GetFlag(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Flag");
            }

            var viewModel = new UpdateFlagViewModel
            {
                Id = response.Data.Id,
                FlagName = response.Data.FlagName,
                Description = response.Data.Description
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(string id, UpdateFlagViewModel request)
        {
            var response = _flagService.UpdateFlag(id, request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View(request);
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Flag");
        }

        [HttpPost]
        public IActionResult DeleteFlag(string id)
        {
            var response = _flagService.DeleteFlag(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Flag");
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Flag");
        }
    }
}
