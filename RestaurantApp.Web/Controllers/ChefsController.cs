using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.Entities;
using RestaurantApp.Web.Models;

namespace RestaurantApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ChefsController : Controller
    {
        private readonly RestaurantDbContext _context;

        public ChefsController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET: Chefs
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _context.Chefs.ToListAsync());
            }
            catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return NotFound();
            }
        }

        // GET: Chefs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chef = await _context.Chefs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chef == null)
            {
                return NotFound();
            }

            return View(chef);
        }

        // GET: Chefs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chefs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Chef chef)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chef);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chef);
        }

        // GET: Chefs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chef = await _context.Chefs.FindAsync(id);
            if (chef == null)
            {
                return NotFound();
            }
            return View(chef);
        }

        // POST: Chefs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Chef chef)
        {
            if (id != chef.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chef);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChefExists(chef.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(chef);
        }

        // GET: Chefs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chef = await _context.Chefs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chef == null)
            {
                return NotFound();
            }

            return View(chef);
        }

        // POST: Chefs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chef = await _context.Chefs.FindAsync(id);
            _context.Chefs.Remove(chef);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChefExists(int id)
        {
            return _context.Chefs.Any(e => e.Id == id);
        }
    }
}
