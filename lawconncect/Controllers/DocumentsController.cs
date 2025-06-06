﻿using System;
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
    public class DocumentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        IWebHostEnvironment _host;


        public DocumentsController(ApplicationDbContext context, IWebHostEnvironment host   )
        {
            _context = context;
            _host = host;
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            return View(await _context.documents.ToListAsync());
        }

        // GET: Documents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documents = await _context.documents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documents == null)
            {
                return NotFound();
            }

            return View(documents);
        }

        // GET: Documents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Document")] Documents documents)
        {
            if (ModelState.IsValid)
            {
                if (documents.Document != null)
                {

                    string ext = Path.GetExtension(documents.Document.FileName).ToLower();
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                    {

                        try
                        {
                            string savePath = Path.Combine(_host.WebRootPath, "Pictures");
                            if (!Directory.Exists(savePath))
                            {
                                Directory.CreateDirectory(savePath);
                            }
                            string filename=Guid.NewGuid().ToString();
                            string filePath = Path.Combine(savePath, filename + ext);
                            using (FileStream fs = new FileStream(filePath, FileMode.Create))
                            {
                                documents.Document.CopyTo(fs);
                            }
                            documents.DocPath = "~/Pictures/" + filename + ext;
                            _context.Add(documents);
                            if (await _context.SaveChangesAsync() > 0)
                            {
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                ModelState.AddModelError("", "Save failed");
                                //  return View(category);
                            }
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", ex.Message);
                            // return View(category);

                        }


                    }
                    else
                    {
                        ModelState.AddModelError("", "Please enter .jpg |.png |.jpeg image");
                        // return View(category);
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Please enter valid image");
                    // return View(category);
                }


                //_context.Add(documents);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return View(documents);
        }

        // GET: Documents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documents = await _context.documents.FindAsync(id);
            if (documents == null)
            {
                return NotFound();
            }
            return View(documents);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DocPath")] Documents documents)
        {
            if (id != documents.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentsExists(documents.Id))
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
            return View(documents);
        }

        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documents = await _context.documents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documents == null)
            {
                return NotFound();
            }

            return View(documents);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documents = await _context.documents.FindAsync(id);
            if (documents != null)
            {
                _context.documents.Remove(documents);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentsExists(int id)
        {
            return _context.documents.Any(e => e.Id == id);
        }
    }
}
