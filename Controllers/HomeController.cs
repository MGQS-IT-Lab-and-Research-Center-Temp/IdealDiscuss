using IdealDiscuss.ActionFilters;
using IdealDiscuss.Models.Auth;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdealDiscuss.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IQuestionService _questionService;

        public HomeController(
            IUserService userService,
            IQuestionService questionService)
        {
            _userService = userService;
            _questionService = questionService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var questions = _questionService.DisplayQuestion();
            ViewData["Message"] = questions.Message;
            ViewData["Status"] = questions.Status;

            return View(questions.Data);
        }

        public IActionResult SignUp()
        {
            ViewData["Message"] = "";
            ViewData["Status"] = false;

            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            var result = _userService.AddUser(model);

            if (result.Status == false)
            {
                ViewData["Message"] = result.Message;
                ViewData["Status"] = result.Status;

                return View();
            }

            ViewData["Message"] = result.Message;
            ViewData["Status"] = result.Status;

            return View();
        }

        [RedirectIfAuthenticated]
        public IActionResult Login()
        {
            ViewData["Message"] = "";
            ViewData["Status"] = false;

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var response = _userService.Login(model.UserName, model.Password);
            var user = response.Data;

            if (response.Status == false)
            {
                ViewData["Message"] = response.Message;
                ViewData["Status"] = response.Status;

                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleName),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authenticationProperties = new AuthenticationProperties();

            var principal = new ClaimsPrincipal(claimsIdentity);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

            if (user.RoleName == "Admin")
            {
                return RedirectToAction("AdminDashboard", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }

        [Authorize(Roles = "Admin")]
        //[RoleAuthorize("Admin")]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}