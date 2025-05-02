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
    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext _context;
        IWebHostEnvironment _host;

        public ClientsController(ApplicationDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,ContactNo,Image,ImagePath,Description")] Client client)
        {
            if (ModelState.IsValid)
            {
                if (client.Image != null)
                {

                    string ext = Path.GetExtension(client.Image.FileName).ToLower();
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                    {

                        try
                        {
                            string savePath = Path.Combine(_host.WebRootPath, "Pictures");
                            if (!Directory.Exists(savePath))
                            {
                                Directory.CreateDirectory(savePath);
                            }
                            string filePath = Path.Combine(savePath, client.Name + ext);
                            using (FileStream fs = new FileStream(filePath, FileMode.Create))
                            {
                                client.Image.CopyTo(fs);
                            }
                            client.ImagePath = "~/Pictures/" + client.Name + ext;
                            _context.Add(client);
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
                        ModelState.AddModelError("", "Please enter .jpg |.png |.jpeg Image");
                        // return View(category);
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Please enter valid image");
                    // return View(category);
                }



                //_context.Add(client);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,ContactNo,ImagePath,Description")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
