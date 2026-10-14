using System.Net.Http.Json;
using ClientBlazorSeries.Models.DTO;

namespace ClientBlazorSeries.Services
{
    public class SerieService : ISerieService
    {
        private readonly HttpClient _httpClient;

      
        public SerieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SerieDto>> GetSeriesAsync(CancellationToken cancellationToken = default)
        {
            var result = await _httpClient.GetFromJsonAsync<List<SerieDto>>("api/series", cancellationToken);
            return result ?? new List<SerieDto>();
        }

        public async Task<SerieDetailDto?> GetSerieByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<SerieDetailDto>($"api/series/{id}", cancellationToken);
        }

        public async Task<bool> UpdateSerieAsync(int id, SerieDetailDto serie, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/series/{id}", serie, cancellationToken);
            return response.IsSuccessStatusCode;
        }
    }
}