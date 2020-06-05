using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Theater.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly DBTheaterContext _context;

        public ChartController(DBTheaterContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var theaters = _context.Performance.Include(b => b.TheaterPerformances).ToList();
            List<object> catPerformance = new List<object>();
            catPerformance.Add(new[] { "Вистава", "Кількість театрів що ставлять" });

            foreach(var c in theaters)
            {
                catPerformance.Add(new object[] { c.PrName, c.TheaterPerformances.Count() });
            }
            return new JsonResult(catPerformance);
        }
    }
}