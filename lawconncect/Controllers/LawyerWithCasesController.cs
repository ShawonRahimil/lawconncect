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
    public class LawyerWithCasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LawyerWithCasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LawyerWithCases
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LawyerWithCase.Include(l => l.Case);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LawyerWithCases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lawyerWithCase = await _context.LawyerWithCase
                .Include(l => l.Case)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lawyerWithCase == null)
            {
                return NotFound();
            }

            return View(lawyerWithCase);
        }

        // GET: LawyerWithCases/Create
        public IActionResult Create()
        {
            ViewData["CaseId"] = new SelectList(_context.Sases, "ID", "ID");
            return View();
        }

        // POST: LawyerWithCases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CaseId,lawyerId")] LawyerWithCase lawyerWithCase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lawyerWithCase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaseId"] = new SelectList(_context.Sases, "ID", "ID", lawyerWithCase.CaseId);
            return View(lawyerWithCase);
        }

        // GET: LawyerWithCases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lawyerWithCase = await _context.LawyerWithCase.FindAsync(id);
            if (lawyerWithCase == null)
            {
                return NotFound();
            }
            ViewData["CaseId"] = new SelectList(_context.Sases, "ID", "ID", lawyerWithCase.CaseId);
            return View(lawyerWithCase);
        }

        // POST: LawyerWithCases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CaseId,lawyerId")] LawyerWithCase lawyerWithCase)
        {
            if (id != lawyerWithCase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lawyerWithCase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LawyerWithCaseExists(lawyerWithCase.Id))
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
            ViewData["CaseId"] = new SelectList(_context.Sases, "ID", "ID", lawyerWithCase.CaseId);
            return View(lawyerWithCase);
        }

        // GET: LawyerWithCases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lawyerWithCase = await _context.LawyerWithCase
                .Include(l => l.Case)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lawyerWithCase == null)
            {
                return NotFound();
            }

            return View(lawyerWithCase);
        }

        // POST: LawyerWithCases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lawyerWithCase = await _context.LawyerWithCase.FindAsync(id);
            if (lawyerWithCase != null)
            {
                _context.LawyerWithCase.Remove(lawyerWithCase);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LawyerWithCaseExists(int id)
        {
            return _context.LawyerWithCase.Any(e => e.Id == id);
        }
    }
}
