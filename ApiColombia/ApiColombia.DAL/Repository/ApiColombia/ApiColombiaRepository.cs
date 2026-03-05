using ApiColombia.DAL.Data;
using ApiColombia.DAL.Repository.ApiColombia.Interfaces;
using ApiColombia.Entities.Entities;
using ApiColombia.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiColombia.DAL.Repository.ApiColombia
{
    public class ApiColombiaRepository : IApiColombiaRepository
    {
        private readonly ApiColombiaContext _context;

        public ApiColombiaRepository(ApiColombiaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
            => await _context.Region.AsNoTracking().ToListAsync();

        public async Task<Region?> GetByIdAsync(int id)
            => await _context.Region.FindAsync(id);

        public async Task AddAsync(Region region)
        {
            await _context.Region.AddAsync(region);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Region region)
        {
            _context.Region.Update(region);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Region region)
        {
            _context.Region.Remove(region);
            await _context.SaveChangesAsync();
        }
    }
}
