using ApiColombia.DAL.Data;
using ApiColombia.DAL.Repository.Interfaces;
using ApiColombia.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiColombia.DAL.Repository
{
    public class ApiColombiaRepository : IApiColombiaRepository
    {
        private readonly ApiColombiaContext _context;

        public ApiColombiaRepository(ApiColombiaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
            => await _context.Regions.AsNoTracking().ToListAsync();

        public async Task<Region?> GetByIdAsync(Guid id)
            => await _context.Regions.FindAsync(id);

        public async Task AddAsync(Region region)
        {
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Region region)
        {
            _context.Regions.Update(region);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Region region)
        {
            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
        }
    }
}
