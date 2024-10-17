using BreakdownApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BreakdownApi.Data
{
    public class BreakdownContext : DbContext
    {
        public BreakdownContext(DbContextOptions<BreakdownContext> options) : base(options)
        {
        }

        public DbSet<Breakdown> Breakdowns { get; set; }
    }
}
