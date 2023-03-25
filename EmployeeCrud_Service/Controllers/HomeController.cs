using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EmployeeCrud_Service.Models;
using System.Security.Claims;

namespace EmployeeCrud_Service.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;


        }


        public IActionResult Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            bool loggedIn = claimUser.Identity.IsAuthenticated;

            int x = 5;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return Redirect("http://localhost:5000/Account/Login");
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return Redirect("http://localhost:5000/Account/Register");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}