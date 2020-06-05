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
    public class PerformancesController : Controller
    {
        private readonly DBTheaterContext _context;

        public PerformancesController(DBTheaterContext context)
        {
            _context = context;
        }

        // GET: Performances
        public IActionResult Index()
        {

            var data = _context.Performance
                 .Include(o => o.PerformanceGenres);

            return View(data);
        }


        // GET: Performances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performance = await _context.Performance
                .FirstOrDefaultAsync(m => m.PrId == id);
            if (performance == null)
            {
                return NotFound();
            }

            return View(performance);
        }

        public async Task<IActionResult> Genres(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PrGn_data = _context.PerformanceGenres
                .Where(o => o.PrId == id).Include(o => o.Gn);
            if (PrGn_data == null)
            {
                return NotFound();
            }
            ViewBag.PrId = id;


            return View(await PrGn_data.ToListAsync());
        }
        
        public async Task<IActionResult> Authors(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PrAu_data = _context.PerformanceAuthor
                .Where(o => o.PrId == id).Include(o => o.Au);
            if (PrAu_data == null)
            {
                return NotFound();
            }
            ViewBag.PrId = id;


            return View(await PrAu_data.ToListAsync());
        }


        // GET: Performances/Create
        public IActionResult Create() => View();


        // POST: Performances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrId,PrName,PrYear,PrInfo")] Performance performance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(performance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(performance);
        }

        public IActionResult AddGenre(int? id)
        {
            if (id != null)
                ViewBag.Back = id;

            ViewData["PrId"] = new SelectList(_context.Performance, "PrId", "PrName", id);
            ViewData["GnId"] = new SelectList(_context.Genres, "GnId", "GnName");
            return View();
        }

        public IActionResult AddAuthor(int? id)
        {
            if (id != null)
                ViewBag.Back = id;

            ViewData["PrId"] = new SelectList(_context.Performance, "PrId", "PrName", id);
            ViewData["AuId"] = new SelectList(_context.Authors, "AuId", "AuName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGenre(int? id, [Bind("PrId,GnId,Info")]PerformanceGenres obj)
        {
            if (id != null)
                ViewBag.Back = id;

            if (ModelState.IsValid)
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
                return RedirectToAction("Genres", new { id = obj.PrId });
            }
            ViewData["PrId"] = new SelectList(_context.Performance, "PrId", "PrName", obj.PrId);
            ViewData["GnId"] = new SelectList(_context.Genres, "GnId", "GnName", obj.GnId);
            return View(obj);
        } 
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAuthor(int? id, [Bind("PrId,AuId,Info")]PerformanceAuthor obj)
        {
            if (id != null)
                ViewBag.Back = id;

            if (ModelState.IsValid)
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
                return RedirectToAction("Authors", new { id = obj.PrId });
            }
            ViewData["PrId"] = new SelectList(_context.Performance, "PrId", "PrName", obj.PrId);
            ViewData["AuId"] = new SelectList(_context.Authors, "AuId", "GnName", obj.AuId);
            return View(obj);
        }


        // GET: Performances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performance = await _context.Performance.FindAsync(id);
            if (performance == null)
            {
                return NotFound();
            }
            return View(performance);
        }

        // POST: Performances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrId,PrName,PrYear,PrInfo")] Performance performance)
        {
            if (id != performance.PrId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(performance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerformanceExists(performance.PrId))
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
            return View(performance);
        }
        public async Task<IActionResult> RemoveGenre(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PrGn_data = await _context.PerformanceGenres
                .Where(o => o.Id == id).Include(o => o.Pr).Include(o => o.Gn)
                .FirstOrDefaultAsync();

            if (PrGn_data == null)
            {
                return NotFound();
            }
            ViewBag.Back = id;

            return View(PrGn_data);
        } 
        
        public async Task<IActionResult> RemoveAuthor(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PrAu_data = await _context.PerformanceAuthor
                .Where(o => o.Id == id).Include(o => o.Pr).Include(o => o.Au)
                .FirstOrDefaultAsync();

            if (PrAu_data == null)
            {
                return NotFound();
            }
            ViewBag.Back = id;

            return View(PrAu_data);
        }


        [HttpPost, ActionName("RemoveGenre")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmedGenre(int id)
        {
            try
            {
                var PrGn_data = await _context.PerformanceGenres.FindAsync(id);
                _context.PerformanceGenres.Remove(PrGn_data);
                await _context.SaveChangesAsync();
                return RedirectToAction("Genres", new { id = PrGn_data.PrId });
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        
        [HttpPost, ActionName("RemoveAuthor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmedAuthor(int id)
        {
            try
            {
                var PrAu_data = await _context.PerformanceAuthor.FindAsync(id);
                _context.PerformanceAuthor.Remove(PrAu_data);
                await _context.SaveChangesAsync();
                return RedirectToAction("Authors", new { id = PrAu_data.PrId });
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Performances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performance = await _context.Performance
                .FirstOrDefaultAsync(m => m.PrId == id);
            if (performance == null)
            {
                return NotFound();
            }

            return View(performance);
        }

        // POST: Performances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var performance = await _context.Performance.FindAsync(id);
            _context.Performance.Remove(performance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerformanceExists(int id)
        {
            return _context.Performance.Any(e => e.PrId == id);
        }
    }
}
