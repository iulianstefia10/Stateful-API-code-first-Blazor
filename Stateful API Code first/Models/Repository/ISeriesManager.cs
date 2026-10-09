using APIfilms.Models.EntityFramework;

using System.Collections.Generic;
using System.Threading.Tasks;
using Stateful_API_Code_first.Models.DTO;

namespace Stateful_API_Code_first.Models.Repository
{
    public interface ISeriesManager
    {
        Task<IEnumerable<Serie>> GetAllAsync();
        Task<Serie> GetByIdAsync(int id);
        Task<IEnumerable<NetworkDto>> GetNetworksStatsAsync();
    }
}