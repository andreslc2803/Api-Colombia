using ApiColombia.Entities.DTO.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiColombia.BL.Interfaces
{
    public interface IApiColombiaServices
    {
        Task<IEnumerable<RegionDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<RegionDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<int> CreateAsync(CreateRegionDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(int id, UpdateRegionDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);

        Task SyncFromExternalAsync(CancellationToken cancellationToken = default);
    }
}
