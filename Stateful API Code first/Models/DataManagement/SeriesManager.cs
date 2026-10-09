using APIfilms.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Stateful_API_Code_first.Models;
using Stateful_API_Code_first.Models.DTO;
using Stateful_API_Code_first.Models.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;




namespace Stateful_API_Code_first.Models.DataManagement
{
    public class SeriesManager : ISeriesManager
    {
        private readonly FilmRatingsDBContext _context;

        public SeriesManager(FilmRatingsDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Serie>> GetAllAsync()
        {
            return await _context.Set<Serie>().ToListAsync();
        }
        public async Task<Serie> GetByIdAsync(int id)
        {
            return await _context.Set<Serie>().FindAsync(id);
        }
        public async Task<IEnumerable<NetworkDto>> GetNetworksStatsAsync()
        {
            return await _context.Set<Serie>()
                .Where(s => s.Network != null) 
                .GroupBy(s => s.Network)
                .Select(g => new NetworkDto
                {
                    NetworkName = g.Key,
                    NbSeries = g.Count(),
                    TotalEpisodes = g.Sum(s => s.NbEpisodes ?? 0)
                })
                .ToListAsync();
        }
    }
}