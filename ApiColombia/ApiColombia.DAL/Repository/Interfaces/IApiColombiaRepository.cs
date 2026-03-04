using ApiColombia.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiColombia.DAL.Repository.Interfaces
{
    public interface IApiColombiaRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(int id);
        Task AddAsync(Region region);
        Task UpdateAsync(Region region);
        Task DeleteAsync(Region region);
    }
}
