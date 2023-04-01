﻿using IdealDiscuss.Models.Role;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdealDiscuss.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
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
        public IActionResult Create(CreateRoleViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = _roleService.CreateRole(request);
            ViewData["Status"] = response.Status;
            ViewData["Message"] = response.Message;

            return View();
        }

        public IActionResult GetRoleDetail(string id)
        {
            var response = _roleService.GetRole(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Role);
        }

        public IActionResult Update(string id)
        {
            var response = _roleService.GetRole(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

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
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteRole([FromRoute] string id)
        {
            var response = _roleService.DeleteRole(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return RedirectToAction("Index");
        }
    }
}
