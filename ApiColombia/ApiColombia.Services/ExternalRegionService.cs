using ApiColombia.Services.Interfaces;
using System.Net.Http.Json;
using ApiColombia.Entities.DTO.ExternalServices;

namespace ApiColombia.Services
{
    /// <summary>
    /// Servicio encargado de consumir la API externa de regiones.
    /// Utiliza HttpClient para obtener todas las regiones o una región específica por Id.
    /// </summary>
    public class ExternalRegionService : IExternalRegionService
    {
        private readonly HttpClient _httpClient;

        public ExternalRegionService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("RegionServiceApi");
        }

        /// <summary>
        /// Obtiene todas las regiones desde la API externa
        /// </summary>
        public async Task<IEnumerable<ExternalRegionDto>> GetRegionsAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync("Region", cancellationToken);

            response.EnsureSuccessStatusCode();

            var regions = await response.Content
                .ReadFromJsonAsync<IEnumerable<ExternalRegionDto>>(cancellationToken: cancellationToken);

            return regions ?? Enumerable.Empty<ExternalRegionDto>();
        }

        /// <summary>
        /// Obtiene una región específica por Id desde la API externa
        /// </summary>
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