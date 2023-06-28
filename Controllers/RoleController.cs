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

        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRole();

            ViewData["Message"] = roles.Message;
            ViewData["Status"] = roles.Status;

            return View(roles.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel request)
        {
            var response = await _roleService.CreateRole(request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);

                return View(request);
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Role");
        }

        public async Task<IActionResult> GetRoleDetail(string id)
        {
            var response = await _roleService.GetRole(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Role");
            }

            return View(response.Data);
        }

        public async Task<IActionResult> Update(string id)
        {
            var response = await _roleService.GetRole(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Role");
            }

            var viewModel = new UpdateRoleViewModel
            {
                Id = response.Data.Id,
                RoleName = response.Data.RoleName,
                Description = response.Data.Description
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, UpdateRoleViewModel request)
        {
            var response = await _roleService.UpdateRole(id, request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View(request);
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Role");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole([FromRoute] string id)
        {
            var response = await _roleService.DeleteRole(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Role"); ;
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Role");
        }
    }
}
