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
    public class VouchersController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44366/api");
        HttpClient client;

        private readonly RestaurantDbContext _context;

        public VouchersController(RestaurantDbContext context)
        {
            _context = context;
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        // GET: Vouchers
        public ActionResult Index()
        {
            try
            {
                List<Vouchers> modelList = new List<Vouchers>();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Vouchers/GetVouchers").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    modelList = JsonConvert.DeserializeObject<List<Vouchers>>(data);
                }

                return View(modelList.ToList());
            }
            catch(Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return NotFound();
            }
        }

        // GET: Vouchers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vouchers = await _context.Vouchers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vouchers == null)
            {
                return NotFound();
            }

            return View(vouchers);
        }

        // GET: Vouchers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vouchers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,VoucherType,Discount,MinimumAmount,IsActive")] Vouchers vouchers)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(vouchers);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            //return View(vouchers);
            string data = JsonConvert.SerializeObject(vouchers);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Vouchers/CreateVoucher", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Vouchers/Edit/5
        public IActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var vouchers = await _context.Vouchers.FindAsync(id);
            //if (vouchers == null)
            //{
            //    return NotFound();
            //}
            //return View(vouchers);

            Vouchers model = new Vouchers();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Vouchers/GetVoucherById?id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                string datamodel = data.Replace("[", string.Empty).Replace("]", string.Empty);
                model = JsonConvert.DeserializeObject<Vouchers>(datamodel);
            }
            //ViewData["ChefId"] = new SelectList(_context.Chefs, "Id", "Name", model.Id);
            return View(model);
        }

        // POST: Vouchers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,VoucherType,Discount,MinimumAmount,IsActive")] Vouchers vouchers)
        {
            //if (id != vouchers.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(vouchers);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!VouchersExists(vouchers.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(vouchers);

            string data = JsonConvert.SerializeObject(vouchers);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Vouchers/UpdateVoucher", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            //ViewData["ChefId"] = new SelectList(_context.Chefs, "Id", "Name", menu.ChefId);
            return View(vouchers);
        }

        // GET: Vouchers/Delete/5
        public IActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var vouchers = await _context.Vouchers
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (vouchers == null)
            //{
            //    return NotFound();
            //}

            //return View(vouchers);

            Vouchers model = new Vouchers();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Vouchers/GetMVoucherById?id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                string datamodel = data.Replace("[", string.Empty).Replace("]", string.Empty);
                model = JsonConvert.DeserializeObject<Vouchers>(datamodel);
            }

            return View(model);
        }

        // POST: Vouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            //var vouchers = await _context.Vouchers.FindAsync(id);
            //_context.Vouchers.Remove(vouchers);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Vouchers/DeleteVoucher?Id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        private bool VouchersExists(int id)
        {
            return _context.Vouchers.Any(e => e.Id == id);
        }
    }
}
