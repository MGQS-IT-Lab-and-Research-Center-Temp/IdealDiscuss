using IdealDiscuss.Dtos.RoleDto;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers
{
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
            var roles = _roleService.GetAllRole().Roles;

            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateRoleDto request)
        {
            var response = _roleService.CreateRole(request);

            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;

            return View(response);
        }
        
        public IActionResult GetRoleDetail(int id) 
        {
            var response = _roleService.GetRole(id);
            return View(response);
        }
       
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(UpdateRoleDto updateRoleDto, int id)
        {
            var roleUpdate = _roleService.UpdateRole(id, updateRoleDto);
            ViewBag.Message = roleUpdate.Message;
            ViewBag.Status = roleUpdate.Status;
            return RedirectToAction("Index");
        }

        [HttpPost("role/{id}/delete")]
        public IActionResult DeleteRole([FromRoute] int id)
        {
            var response = _roleService.DeleteRole(id);
            ViewBag.Message = response.Message;
            ViewBag.Status = response.Status;
            return RedirectToAction("Index", "Role");
        }
    }
}
