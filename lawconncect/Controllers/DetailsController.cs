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
    public class DetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Details
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.details.Include(d => d.Documents).Include(d => d.LawyerWithCase).Include(d => d.Master);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Details/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = await _context.details
                .Include(d => d.Documents)
                .Include(d => d.LawyerWithCase)
                .Include(d => d.Master)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (details == null)
            {
                return NotFound();
            }

            return View(details);
        }

        // GET: Details/Create
        public IActionResult Create()
        {
            ViewData["DocumentsId"] = new SelectList(_context.documents, "Id", "Id");
            ViewData["LawyerWithCaseId"] = new SelectList(_context.LawyerWithCase, "Id", "Id");
            ViewData["MasterId"] = new SelectList(_context.Masters, "Id", "Id");
            return View();
        }

        // POST: Details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HieringDate,NextHieringDate,Hiering,Comments,DocumentsId,LawyerWithCaseId,MasterId")] Details details)
        {
            if (ModelState.IsValid)
            {
                _context.Add(details);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DocumentsId"] = new SelectList(_context.documents, "Id", "Id", details.DocumentsId);
            ViewData["LawyerWithCaseId"] = new SelectList(_context.LawyerWithCase, "Id", "Id", details.LawyerWithCaseId);
            ViewData["MasterId"] = new SelectList(_context.Masters, "Id", "Id", details.MasterId);
            return View(details);
        }

        // GET: Details/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = await _context.details.FindAsync(id);
            if (details == null)
            {
                return NotFound();
            }
            ViewData["DocumentsId"] = new SelectList(_context.documents, "Id", "Id", details.DocumentsId);
            ViewData["LawyerWithCaseId"] = new SelectList(_context.LawyerWithCase, "Id", "Id", details.LawyerWithCaseId);
            ViewData["MasterId"] = new SelectList(_context.Masters, "Id", "Id", details.MasterId);
            return View(details);
        }

        // POST: Details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HieringDate,NextHieringDate,Hiering,Comments,DocumentsId,LawyerWithCaseId,MasterId")] Details details)
        {
            if (id != details.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(details);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailsExists(details.Id))
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
            ViewData["DocumentsId"] = new SelectList(_context.documents, "Id", "Id", details.DocumentsId);
            ViewData["LawyerWithCaseId"] = new SelectList(_context.LawyerWithCase, "Id", "Id", details.LawyerWithCaseId);
            ViewData["MasterId"] = new SelectList(_context.Masters, "Id", "Id", details.MasterId);
            return View(details);
        }

        // GET: Details/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = await _context.details
                .Include(d => d.Documents)
                .Include(d => d.LawyerWithCase)
                .Include(d => d.Master)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (details == null)
            {
                return NotFound();
            }

            return View(details);
        }

        // POST: Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var details = await _context.details.FindAsync(id);
            if (details != null)
            {
                _context.details.Remove(details);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetailsExists(int id)
        {
            return _context.details.Any(e => e.Id == id);
        }
    }
}
