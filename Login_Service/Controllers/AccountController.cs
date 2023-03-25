using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Login_Service.Models;
using Login_Service.Data;
using Microsoft.EntityFrameworkCore;
using Login_Service.Models.Domain;

namespace Login_Service.Controllers
{
    public class AccountController : Controller
    {
        public AuthDbContext ApplicationDbContext { get; }

        public AccountController(AuthDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                //return RedirectToAction("Index", "Home");
                return Redirect("http://localhost:5100/Home/Index");
            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel modelRegister)
        {
            if (!ModelState.IsValid)
                return View();


            //Search if username and password exists
            var users = await ApplicationDbContext.Users.ToListAsync();
            bool userExists = users.Find(x => x.Username == modelRegister.Username) != null;

            if (userExists)
            {
                ViewData["ExistsMessage"] = "user already exists";
                return View();
            }

            var user = new User()
            {
                Username = modelRegister.Username,
                Password = modelRegister.Password

            };

            await ApplicationDbContext.Users.AddAsync(user);
            await ApplicationDbContext.SaveChangesAsync();
            await LoginUser(modelRegister.Username, modelRegister.Password);

            return Redirect("http://localhost:5100/Home/Index");
            //return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel modelLogin)
        {
            //Search if username and password exists
            var users = await ApplicationDbContext.Users.ToListAsync();
            bool validUser = users.Find(x => x.Username == modelLogin.Username && x.Password == modelLogin.Password) != null;

            if (validUser)
            {
                await LoginUser(modelLogin.Username, modelLogin.Password, modelLogin.KeepLoggedIn);

                return Redirect("http://localhost:5100/");
                //return RedirectToAction("Index", "Home");
            }

            ViewData["ValidateMessage"] = "user not found";
            return View();
        }

        private async Task LoginUser(string username, string password, bool keepLoggedIn = true)
        {
            List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, username),
                    new Claim("OtherProperties", "Example Role")
                };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = keepLoggedIn
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties);
        }


        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
