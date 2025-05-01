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
    public class LawyersController : Controller
    {
        private readonly ApplicationDbContext _context;
      IWebHostEnvironment _host;

        public LawyersController(ApplicationDbContext context , IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // GET: Lawyers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lawyers.ToListAsync());
        }

        // GET: Lawyers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lawyer = await _context.Lawyers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lawyer == null)
            {
                return NotFound();
            }

            return View(lawyer);
        }

        // GET: Lawyers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lawyers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,ContactNo,Images,Description")] Lawyer lawyer)
        {
            if (ModelState.IsValid)
            {
                if (lawyer.Image != null)
                {
                    string ext = Path.GetExtension(lawyer.Image.FileName).ToLower();
                    if (ext==".jpg"||ext==".png"||ext=="jpeg")
                    {
                        string savePath = Path.Combine(_host.WebRootPath, "Pictures");
                        if (!Directory.Exists(savePath))
                        {
                            Directory.CreateDirectory(savePath);
                        }
                        string filePath = Path.Combine(savePath, lawyer.Name + ext);
                        using (FileStream fs=new FileStream(filePath,FileMode.Create))
                        {
                            lawyer.Image.CopyTo(fs);

                        }
                        lawyer.Images = "~/Pictures/" + lawyer.Name + ext;
                        _context.Add(lawyer);
                        if(await _context.SaveChangesAsync() > 0)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Save Failed");
                        }


                    }
                    else
                    {
                        ModelState.AddModelError("", "Please Choose Image typer jpg||jpeg||png ");


                    }

                }
                else
                {
                    ModelState.AddModelError("", "Please Choose a Image");
                }




                //_context.Add(lawyer);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            
            return View(lawyer);
        }

        // GET: Lawyers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lawyer = await _context.Lawyers.FindAsync(id);
            if (lawyer == null)
            {
                return NotFound();
            }
            return View(lawyer);
        }

        // POST: Lawyers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,ContactNo,Images,Description")] Lawyer lawyer)
        {
            if (id != lawyer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lawyer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LawyerExists(lawyer.Id))
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
            return View(lawyer);
        }

        // GET: Lawyers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lawyer = await _context.Lawyers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lawyer == null)
            {
                return NotFound();
            }

            return View(lawyer);
        }

        // POST: Lawyers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lawyer = await _context.Lawyers.FindAsync(id);
            if (lawyer != null)
            {
                _context.Lawyers.Remove(lawyer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LawyerExists(int id)
        {
            return _context.Lawyers.Any(e => e.Id == id);
        }
    }
}
