using RPG_DOTNET_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace RPG_DOTNET_MVC.Controllers
{
    /*[Authorize]*/
    public class CharacterController : Controller
    {
        /*private readonly ApplicationDbContext _db;*/
        private readonly IHttpClientFactory _httpClientFactory;
        public CharacterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int pg = 1, string SearchText = "", string SortOrder = "")
        {
            const int pageSize = 5;
            var client = _httpClientFactory.CreateClient("myapi");
            var token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            /*var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");*/
            HttpResponseMessage responseMessage = await client.GetAsync("character/getall?PageNumber="+pg+"&PageSize="+pageSize);

            var responseBody = await responseMessage.Content.ReadAsStringAsync();
            var jsonDataDeserializeObject = JsonConvert.DeserializeObject<ServiceResponseCharacter>(responseBody);
            if (responseMessage.IsSuccessStatusCode)
            {
                if (SearchText != null && SearchText != "")
                {
                    responseMessage = await client.GetAsync("character/search/" + SearchText);
                    responseBody = await responseMessage.Content.ReadAsStringAsync();
                    jsonDataDeserializeObject = JsonConvert.DeserializeObject<ServiceResponseCharacter>(responseBody);
                    /*objcategoriesList = objcategoriesList.Where(s => s.Name.Contains(SearchText));*/
                }
                return View(jsonDataDeserializeObject.Data);
                /*ViewData["NameSort"] = String.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
                var objcategoriesList = from c in _db.Categories
                           select c;
                switch (SortOrder)
                {
                    case "name_desc":
                        objcategoriesList = objcategoriesList.OrderByDescending(a => a.Name);
                        break;
                    default:
                        objcategoriesList = objcategoriesList.OrderBy(a => a.Name);
                        break;
                }

               

                
                if (pg < 1)
                {
                    pg = 1;
                }*/
                /*int recsCount = jsonDataDeserializeObject.Data.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var data = jsonDataDeserializeObject.Data.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                return View(data);*/
                
            }
                return View();
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        *//* public IActionResult Create(Category obj)*//*
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }*/

        //GET
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            /*var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);*/
            return View();
        }

        //POST
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                *//*_db.Categories.Update(obj);
                _db.SaveChanges();*//*
                TempData["success"] = "Category Updated Successfully!";
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
            /*var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }*/
           /* return View(categoryFromDb);*/
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
           /* var obj = _db.Categories.Find(id);
                if (obj == null)
                {
                    return NotFound();
                }
            _db.Categories.Remove(obj);
            _db.SaveChanges();*/
            TempData["success"] = "Character Deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}
