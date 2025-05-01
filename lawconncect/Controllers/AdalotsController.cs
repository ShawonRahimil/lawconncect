using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lawconncect.Data;
using lawconncect.Models;

namespace lawconncect.Controllers
{
    public class AdalotsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdalotsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Adalots
        public async Task<IActionResult> Index()
        {
            return View(await _context.adalots.ToListAsync());
        }

        // GET: Adalots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adalot = await _context.adalots
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adalot == null)
            {
                return NotFound();
            }

            return View(adalot);
        }

        // GET: Adalots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adalots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Location")] Adalot adalot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adalot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adalot);
        }

        // GET: Adalots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adalot = await _context.adalots.FindAsync(id);
            if (adalot == null)
            {
                return NotFound();
            }
            return View(adalot);
        }

        // POST: Adalots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location")] Adalot adalot)
        {
            if (id != adalot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adalot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdalotExists(adalot.Id))
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
            return View(adalot);
        }

        // GET: Adalots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adalot = await _context.adalots
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adalot == null)
            {
                return NotFound();
            }

            return View(adalot);
        }

        // POST: Adalots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adalot = await _context.adalots.FindAsync(id);
            if (adalot != null)
            {
                _context.adalots.Remove(adalot);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdalotExists(int id)
        {
            return _context.adalots.Any(e => e.Id == id);
        }
    }
}
