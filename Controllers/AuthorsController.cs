using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Theater;

namespace Theater.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly DBTheaterContext _context;

        public AuthorsController(DBTheaterContext context)
        {
            _context = context;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var dBTheaterContext = _context.Authors.Include(a => a.AuCtNavigation);
            return View(await dBTheaterContext.ToListAsync());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authors = await _context.Authors
                .Include(a => a.AuCtNavigation)
                .FirstOrDefaultAsync(m => m.AuId == id);
            if (authors == null)
            {
                return NotFound();
            }

            return View(authors);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            ViewData["AuCt"] = new SelectList(_context.Cities, "CtId", "CtName");
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuId,AuName,AuDateb,AuDated,AuCt,AuInfo")] Authors authors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuCt"] = new SelectList(_context.Cities, "CtId", "CtName", authors.AuCt);
            return View(authors);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authors = await _context.Authors.FindAsync(id);
            if (authors == null)
            {
                return NotFound();
            }
            ViewData["AuCt"] = new SelectList(_context.Cities, "CtId", "CtName", authors.AuCt);
            return View(authors);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuId,AuName,AuDateb,AuDated,AuCt,AuInfo")] Authors authors)
        {
            if (id != authors.AuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorsExists(authors.AuId))
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
            ViewData["AuCt"] = new SelectList(_context.Cities, "CtId", "CtName", authors.AuCt);
            return View(authors);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authors = await _context.Authors
                .FirstOrDefaultAsync(m => m.AuId == id);
            if (authors == null)
            {
                return NotFound();
            }

            return View(authors);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authors = await _context.Authors.FindAsync(id);
            _context.Authors.Remove(authors);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorsExists(int id)
        {
            return _context.Authors.Any(e => e.AuId == id);
        }
    }
}
