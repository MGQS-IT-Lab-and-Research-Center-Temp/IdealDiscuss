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

        [HttpGet("flag")]
        public IActionResult Index()
        {
            var instances = _flagService.GetAllFlag();
            return View(instances);
        }

        [HttpPost("flag/create")]
        public IActionResult CreateFlag([FromForm] CreateFlagDto createFlagDto)
        {
            var response = _flagService.CreateFlag(createFlagDto);
            return RedirectToAction("ViewMembers", "Member");
        }

        [HttpGet("flag/{id}")]
        public IActionResult GetFlagDetail(int id)
        {
            var response = _flagService.GetFlag(id);
            return View(response);
        }

        [HttpGet("flag/{id}/edit")]
        public IActionResult EditFlag(int id)
        {
            var instance = _flagService.GetFlag(id);
            return View(instance);
        }


        [HttpPost("flag/{id}/edit")]
        public IActionResult EditFlag([FromRoute] int id, [FromForm] UpdateFlagDto updateFlagDto)
        {
            var response = _flagService.UpdateFlag(id, updateFlagDto);
            return RedirectToAction("Index", "Flag");
        }

        [HttpPost("flag/{id}/delete")]
        public IActionResult DeleteFlag([FromRoute] int id)
        {
            var response = _flagService.DeleteFlag(id);
            return RedirectToAction("Index", "Flag");
        }
    }
}
