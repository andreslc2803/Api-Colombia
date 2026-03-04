using ApiColombia.Services.Interfaces;
using ApiColombia.Entities.DTO;
using System.Net.Http.Json;

namespace ApiColombia.Services
{
    public class ExternalRegionService : IExternalRegionService
    {
        private readonly HttpClient _httpClient;

        public ExternalRegionService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("RegionServiceApi");
        }

        public async Task<IEnumerable<ExternalRegionDto>> GetRegionsAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync("Region", cancellationToken);

            response.EnsureSuccessStatusCode();

            var regions = await response.Content
                .ReadFromJsonAsync<IEnumerable<ExternalRegionDto>>(cancellationToken: cancellationToken);

            return regions ?? Enumerable.Empty<ExternalRegionDto>();
        }

        public async Task<ExternalRegionDto?> GetRegionByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"Region/{id}", cancellationToken);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content
                .ReadFromJsonAsync<ExternalRegionDto>(cancellationToken: cancellationToken);
        }
    }
}