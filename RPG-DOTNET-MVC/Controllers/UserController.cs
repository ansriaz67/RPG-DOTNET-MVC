using RPG_DOTNET_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace RPG_DOTNET_MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        /*private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }*/
        private readonly IHttpClientFactory _httpClientFactory;
        
        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            /*var userList = from c in _db.RegisterModel
                                    select c;*/
            var client = _httpClientFactory.CreateClient("myapi");
            var token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage responseMessage = await client.GetAsync("");
            var responseBody = await responseMessage.Content.ReadAsStringAsync();
            var jsonDataDeserializeObject = JsonConvert.DeserializeObject<ServiceResponseCharacter>(responseBody);
            return View();
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
       /* [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RegisterModel obj)
        {
            *//*var chkEmail = _db.RegisterModel.Where(e => e.UserName == obj.UserName).SingleOrDefault();*//*
            
            if (obj.Password != obj.ConfirmPassword.ToString())
            {
                ModelState.AddModelError("name", "The Password does not match.");
            }
            else if (obj.UserName == "" || obj.Name == "" || obj.Password == "" || obj.ConfirmPassword == "")
            {
                ModelState.AddModelError("name", "Field is required.");
            }
            else if (ModelState.IsValid)
            {
                *//*_db.RegisterModel.Add(obj);
                _db.SaveChanges();*//*
                TempData["success"] = "User Created Successfully!";
                return RedirectToAction("Index", "User");
            }
            return View(obj);
        }*/


        //GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            /* var userFromDb = _db.RegisterModel.Find(id);
             if (userFromDb == null)
             {
                 return NotFound();
             }*/
            /*return View(userFromDb);*/

            var client = _httpClientFactory.CreateClient("myapi");
            var token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage responseMessage = await client.GetAsync("");
            var responseBody = await responseMessage.Content.ReadAsStringAsync();
            var jsonDataDeserializeObject = JsonConvert.DeserializeObject<ServiceResponseCharacter>(responseBody);
            return View(jsonDataDeserializeObject.Data);
        }

        //POST
       /* [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(RegisterModel obj)
        {
            if (obj.Password != obj.ConfirmPassword.ToString())
            {
                ModelState.AddModelError("name", "The Password does not match.");
            }
            if (ModelState.IsValid)
            {
                    *//*_db.RegisterModel.Update(obj);
                _db.SaveChanges();*//*
                TempData["success"] = "User Updated Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }*/

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
           /* var userFromDb = _db.RegisterModel.Find(id);
            if (userFromDb == null)
            {
                return NotFound();
            }*/
            /*return View(userFromDb);*/
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            /*var obj = _db.RegisterModel.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.RegisterModel.Remove(obj);
            _db.SaveChanges();*/
            TempData["success"] = "User Deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}
