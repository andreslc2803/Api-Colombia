using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiColombia.Entities.DTO;

namespace ApiColombia.Services.Interfaces
{
    public interface IExternalRegionService
    {
        Task<IEnumerable<ExternalRegionDto>> GetRegionsAsync(CancellationToken cancellationToken = default);
        Task<ExternalRegionDto?> GetRegionByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
