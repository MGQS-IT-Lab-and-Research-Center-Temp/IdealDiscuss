using IdealDiscuss.Dtos.UserDto;
using IdealDiscuss.Models;
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
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUserService userService,IQuestionService questionService)
        {
            _logger = logger;
            _userService = userService;
            _questionService = questionService;
        }
        [Authorize]
        public IActionResult Index()
        {
            var questions = _questionService.DisplayQuestion();
            return View(questions.Questions);
        }
        
        public IActionResult SignUp()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            var user = new CreateUserDto
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
            };

            var result = _userService.AddUser(user);

            if (result.Status == false)
            {
                ViewBag.Message = result.Message;
                return View();
            }

            return View(result);
        }

        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var user = _userService.Login(model.UserName, model.Password);

            if (user.Status == false)
            {
                ViewBag.Message = user.Message;
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
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

        [Authorize]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [Authorize(Roles ="Admin")]
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