using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BreakdownApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreakdownController : ControllerBase
    {
        private readonly BreakdownContext _context;

        public BreakdownController(BreakdownContext context)
        {
            _context = context;
        }

        // GET api/breakdowns  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Breakdown>>> GetBreakdowns()
        {
            return await _context.Breakdowns.ToListAsync();
        }

        // GET api/breakdowns/5  
        [HttpGet("{id}")]
        public async Task<ActionResult<Breakdown>> GetBreakdown(string id)
        {
            var breakdown = await _context.Breakdowns.FindAsync(id);

            if (breakdown == null)
            {
                return NotFound();
            }

            return breakdown;
        }

        // POST api/breakdowns  
        [HttpPost]
        public async Task<ActionResult<Breakdown>> CreateBreakdown(Breakdown breakdown)
        {
            _context.Breakdowns.Add(breakdown);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBreakdown), new { id = breakdown.BreakdownReference }, breakdown);
        }

        // PUT api/breakdowns/5  
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBreakdown(string id, Breakdown breakdown)
        {
            if (id != breakdown.BreakdownReference)
            {
                return BadRequest();
            }

            _context.Entry(breakdown).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BreakdownExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private async Task<bool> BreakdownExists(string id)
        {
            return await _context.Breakdowns.AnyAsync(e => e.BreakdownReference == id);
        }
    }

    public class Breakdown
    {
        public string BreakdownReference { get; set; }
        public string CompanyName { get; set; }
        public string DriverName { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime BreakdownDate { get; set; }
    }

    public class BreakdownContext : DbContext
    {
        public BreakdownContext(DbContextOptions<BreakdownContext> options) : base(options)
        {
        }

        public DbSet<Breakdown> Breakdowns { get; set; }
    }
}
