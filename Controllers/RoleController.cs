using IdealDiscuss.Dtos.RoleDto;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RoleController> _logger;

        public RoleController(ILogger<RoleController> logger, IRoleService roleService)
        {
            _logger = logger;
            _roleService = roleService;
        }
    
        public IActionResult Index()
        {
            var roles = _roleService.GetAllRole();

            ViewData["Message"] = roles.Message;
            ViewData["Status"] = roles.Status;

            return View(roles.Roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateRoleDto request)
        {
            var response = _roleService.CreateRole(request);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response);
        }
        
        public IActionResult GetRoleDetail(int id) 
        {
            var response = _roleService.GetRole(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Role);
        }

        public IActionResult Update(int id)
        {
            var response = _roleService.GetRole(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Role);
        }

        [HttpPost]
        public IActionResult Update(int id, UpdateRoleDto updateRoleDto)
        {
            var response = _roleService.UpdateRole(id, updateRoleDto);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteRole([FromRoute] int id)
        {
            var response = _roleService.DeleteRole(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return RedirectToAction("Index");
        }
    }
}
