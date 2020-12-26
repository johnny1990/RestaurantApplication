using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestaurantApp.Entities;
using RestaurantApp.Web.Models;

namespace RestaurantApp.Web.Controllers
{
    [Authorize]
    public class MenusController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44366/api");
        HttpClient client;

        private readonly RestaurantDbContext _context;

        public MenusController(RestaurantDbContext context)
        {
            _context = context;

            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        // GET: Menus
        public IActionResult Index()
        {           
            try
            {
                List<Menu> modelList = new List<Menu>();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Menus/GetMenus").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    modelList = JsonConvert.DeserializeObject<List<Menu>>(data);
                }

                return View( modelList.ToList());

            }
            catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return NotFound();
            }
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .Include(m => m.Chef)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
            ViewData["ChefId"] = new SelectList(_context.Chefs, "Id", "Name");

            List<string> mlist = new List<string>();
            ViewBag.Meal = new SelectList(_context.Meals, "Id", "Name");
            foreach (var item in ViewBag.Meal)
            {
                mlist.Add(item.Text + ", ");
            }
            ViewBag.Meals = mlist;

            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,ChefId,Price,Meals")] Menu menu)
        {

            string data = JsonConvert.SerializeObject(menu);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Menus/CreateMenu", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Menus/Edit/5
        public IActionResult Edit(int? id)
        {
            Menu model = new Menu();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Menus/GetMenuById?id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                string datamodel = data.Replace("[", string.Empty).Replace("]", string.Empty);
                model = JsonConvert.DeserializeObject<Menu>(datamodel);
            }
            ViewData["ChefId"] = new SelectList(_context.Chefs, "Id", "Name", model.ChefId);
            return View(model);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Name,ChefId,Price,Meals")] Menu menu)
        {

            string data = JsonConvert.SerializeObject(menu);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Menus/UpdateMenu", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ViewData["ChefId"] = new SelectList(_context.Chefs, "Id", "Name", menu.ChefId);
            return View(menu);
        }

        // GET: Menus/Delete/5
        public IActionResult Delete(int? id)
        {

            Menu model = new Menu();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Menus/GetMenuById?id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                string datamodel = data.Replace("[", string.Empty).Replace("]", string.Empty);
                model = JsonConvert.DeserializeObject<Menu>(datamodel);
            }

            return View(model);

        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Menus/DeleteMenu?Id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }
    }
}
