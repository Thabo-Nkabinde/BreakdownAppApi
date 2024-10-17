using BreakdownApi.Data;
using BreakdownApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BreakdownApi.Services
{
    public class BreakdownService : IBreakdownService
    {
        private readonly BreakdownContext _context;

        public BreakdownService(BreakdownContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Breakdown>> GetBreakdownsAsync()
        {
            return await _context.Breakdowns.ToListAsync();
        }

        public async Task<Breakdown> GetBreakdownAsync(string id)
        {
            return await _context.Breakdowns.FindAsync(id);
        }

        public async Task<Breakdown> CreateBreakdownAsync(Breakdown breakdown)
        {
            _context.Breakdowns.Add(breakdown);
            await _context.SaveChangesAsync();
            return breakdown;
        }

        public async Task UpdateBreakdownAsync(string id, Breakdown breakdown)
        {
            if (id != breakdown.BreakdownReference)
            {
                throw new ArgumentException("Invalid breakdown reference");
            }

            _context.Entry(breakdown).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
