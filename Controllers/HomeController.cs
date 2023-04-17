using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebAppMVCLesson1.Models;

namespace WebAppMVCLesson1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EFContext _db;

        public HomeController(ILogger<HomeController> logger, EFContext db)
        {
            _logger = logger;
            _db = db;
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Location()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _db.Users.FirstOrDefault(x => x.Username.Equals(username) && x.Password.Equals(password));
            if (user!=null)
            {
                var claims = new List<Claim> //утверждения позваляют хранить приватные данные (не обращяться в базу данных)
                {
                    new Claim(ClaimTypes.Name, username),
                };

                var claimsidentity = new ClaimsIdentity(claims, "Login");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsidentity));

                return Redirect("/Secured"); //на какой ссылке ты был на ту и попадешь
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Home/Index");
        }

        
    }
}