using RPG_DOTNET_MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using SendGrid.Helpers.Mail;

namespace RPG_DOTNET_MVC.Controllers
{
    public class LoginController : Controller
    {
        /* private readonly ApplicationDbContext _db;*/
        private readonly IHttpClientFactory _httpClientFactory;


        /*public LoginController(ApplicationDbContext db, IHttpClientFactory httpClientFactory)*/
        public LoginController( IHttpClientFactory httpClientFactory)
        {
            /*_db = db;*/
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginModel obj)
        {
            var client = _httpClientFactory.CreateClient("myapi");
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync("user/login", content);

            var responseBody = await responseMessage.Content.ReadAsStringAsync();
            var jsonDataDeserializeObject = JsonConvert.DeserializeObject<ServiceResponce>(responseBody);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["success"] = "Logged In.";
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, obj.Email) }, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                var token = jsonDataDeserializeObject.Data;
                HttpContext.Session.SetString("token", token);
                return RedirectToAction("Index", "Home");
            }
            /*TempData["error"] = "Something went wrong.";*/
            TempData["error"] = jsonDataDeserializeObject.Message;
            return View();

        }

        public IActionResult Signout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User obj)
        {
            var client = _httpClientFactory.CreateClient("myapi");
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync("user/register", content);

            var responseBody = await responseMessage.Content.ReadAsStringAsync();
            var jsonDataDeserializeObject = JsonConvert.DeserializeObject<ServiceResponce>(responseBody);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["success"] = jsonDataDeserializeObject.Message;
                return RedirectToAction("Index");
            }

            TempData["error"] = jsonDataDeserializeObject.Message;
            return View();
        }
    }
}
