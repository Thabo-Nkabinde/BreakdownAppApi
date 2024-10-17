using BreakdownApi.Models;

public interface IBreakdownService
{
    Task<IEnumerable<Breakdown>> GetBreakdownsAsync();
    Task<Breakdown> GetBreakdownAsync(string id);
    Task<Breakdown> CreateBreakdownAsync(Breakdown breakdown);
    Task UpdateBreakdownAsync(string id, Breakdown breakdown);
}
