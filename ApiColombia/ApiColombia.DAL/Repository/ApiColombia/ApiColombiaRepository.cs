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
    /// <summary>
    /// Repositorio encargado de acceder a la información de las regiones en la base de datos.
    /// Implementa operaciones CRUD básicas.
    /// </summary>
    public class ApiColombiaRepository : IApiColombiaRepository
    {
        private readonly ApiColombiaContext _context;

        public ApiColombiaRepository(ApiColombiaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las regiones de la base de datos.
        /// AsNoTracking se usa para mejorar el rendimiento cuando no se necesita modificar los registros.
        /// </summary>
        public async Task<IEnumerable<Region>> GetAllAsync()
            => await _context.Region.AsNoTracking().ToListAsync();

        /// <summary>
        /// Obtiene una región por su Id.
        /// Devuelve null si no existe.
        /// </summary>
        public async Task<Region?> GetByIdAsync(int id)
            => await _context.Region.FindAsync(id);

        /// <summary>
        /// Agrega una nueva región a la base de datos.
        /// </summary>
        public async Task AddAsync(Region region)
        {
            await _context.Region.AddAsync(region);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Actualiza una región existente en la base de datos.
        /// </summary>
        public async Task UpdateAsync(Region region)
        {
            _context.Region.Update(region);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Elimina una región de la base de datos.
        /// </summary>
        public async Task DeleteAsync(Region region)
        {
            _context.Region.Remove(region);
            await _context.SaveChangesAsync();
        }
    }
}
