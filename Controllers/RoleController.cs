using AspNetCoreHero.ToastNotification.Abstractions;
using IdealDiscuss.Models.Role;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly INotyfService _notyf;
        public RoleController(IRoleService roleService, INotyfService notyf)
        {
            _roleService = roleService;
            _notyf = notyf;
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
        public IActionResult Create(CreateRoleViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var response = _roleService.CreateRole(request);
            if (response.Status is false)
            {
                return View(response);
            }
            _notyf.Success(response.Message);
            return RedirectToAction("Index");
        }

        public IActionResult GetRoleDetail(string id)
        {
            var response = _roleService.GetRole(id);
            if (response.Status is false)
            {
                return View(response);
            }
            _notyf.Success(response.Message);
            return RedirectToAction("Index");
        }

        public IActionResult Update(string id)
        {
            var response = _roleService.GetRole(id);
            if (response.Status is false)
            {
                return View(response);
            }
            _notyf.Success(response.Message);         

            var viewModel = new UpdateRoleViewModel
            {
                Id = response.Role.Id,
                RoleName = response.Role.RoleName,
                Description = response.Role.Description
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(string id, UpdateRoleViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = _roleService.UpdateRole(id, request);
            if (response.Status is false)
            {
                return View(response);
            }
            _notyf.Success(response.Message);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteRole([FromRoute] string id)
        {
            var response = _roleService.DeleteRole(id);
            if (response.Status is false)
            {
                return View(response);
            }
            _notyf.Success(response.Message);
            return RedirectToAction("Index");
        }
    }
}
