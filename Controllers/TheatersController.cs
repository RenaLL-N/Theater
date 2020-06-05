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
    public class TheatersController : Controller
    {
        private readonly DBTheaterContext _context;

        public TheatersController(DBTheaterContext context)
        {
            _context = context;
        }

        // GET: Theaters
        public async Task<IActionResult> Index()
        {
            var dBTheaterContext = _context.Theaters.Include(t => t.ThCtNavigation);
            return View(await dBTheaterContext.ToListAsync());
        }

        // GET: Theaters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theaters = await _context.Theaters
                .Include(t => t.ThCtNavigation)
                .FirstOrDefaultAsync(m => m.ThId == id);
            if (theaters == null)
            {
                return NotFound();
            }

            // return View(theaters);
            return RedirectToAction("Index", "Performances", new { id = theaters.ThId, name = theaters.ThName });
        }
        public async Task<IActionResult> Performance(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PRTH_data = _context.TheaterPerformances
                .Where(o => o.ThId == id).Include(o => o.Pr);
            if (PRTH_data == null)
            {
                return NotFound();
            }
            ViewBag.ThId = id;


            return View(await PRTH_data.ToListAsync());
        }

        public IActionResult AddPerformance(int? id)
        {
            if (id != null)
                ViewBag.Back = id;
            ViewData["ThId"] = new SelectList(_context.Theaters, "ThId", "ThName", id);
            ViewData["PrId"] = new SelectList(_context.Performance, "PrId", "PrName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPerformance(int? id, [Bind("ThId,PrId,Info")]TheaterPerformances obj)
        {
            if (id != null)
                ViewBag.Back = id;

            if (ModelState.IsValid)
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
                return RedirectToAction("Performance", new { id = obj.ThId });
            }
            ViewData["ThId"] = new SelectList(_context.Theaters, "ThId", "ThName", obj.ThId);
            ViewData["PrId"] = new SelectList(_context.Performance, "PrId", "PrName", obj.PrId);
            return View(obj);
        }



        // GET: Theaters/Create
        public IActionResult Create()
        {
            ViewData["ThCt"] = new SelectList(_context.Cities, "CtId", "CtName");
            return View();
        }

        // POST: Theaters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThId,ThName,ThCt,ThWebsite,ThInfo")] Theaters theaters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(theaters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ThCt"] = new SelectList(_context.Cities, "CtId", "CtName", theaters.ThCt);
            return View(theaters);
        }

        // GET: Theaters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theaters = await _context.Theaters.FindAsync(id);
            if (theaters == null)
            {
                return NotFound();
            }
            ViewData["ThCt"] = new SelectList(_context.Cities, "CtId", "CtName", theaters.ThCt);
            return View(theaters);
        }

        // POST: Theaters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThId,ThName,ThCt,ThWebsite,ThInfo")] Theaters theaters)
        {
            if (id != theaters.ThId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theaters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TheatersExists(theaters.ThId))
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
            ViewData["ThCt"] = new SelectList(_context.Cities, "CtId", "CtName", theaters.ThCt);
            return View(theaters);
        }

        public async Task<IActionResult> RemovePerformance(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prth_data = await _context.TheaterPerformances
                .Where(o => o.Id == id).Include(o=>o.Pr).Include(o=>o.Th)
                .FirstOrDefaultAsync();

            if (prth_data == null)
            {
                return NotFound();
            }
            ViewBag.Back = id;

            return View(prth_data);
        }


        [HttpPost, ActionName("RemovePerformance")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmedPerformace(int id)
        {
            try
            {
                var THPR_data = await _context.TheaterPerformances.FindAsync(id);
                _context.TheaterPerformances.Remove(THPR_data);
                await _context.SaveChangesAsync();
                return RedirectToAction("Performance", new { id = THPR_data.ThId });
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }


        // GET: Theaters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theaters = await _context.Theaters
                .Include(t => t.ThCtNavigation)
                .FirstOrDefaultAsync(m => m.ThId == id);
            if (theaters == null)
            {
                return NotFound();
            }

            return View(theaters);
        }

        // POST: Theaters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var theaters = await _context.Theaters.FindAsync(id);
            _context.Theaters.Remove(theaters);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TheatersExists(int id)
        {
            return _context.Theaters.Any(e => e.ThId == id);
        }
    }
}
