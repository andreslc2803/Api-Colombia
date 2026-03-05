using ApiColombia.DAL.Data;
using ApiColombia.DAL.Repository.Auth.Interfaces;
using ApiColombia.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiColombia.DAL.Repository.Auth
{
    /// <summary>
    /// Repositorio encargado de acceder a la información de usuarios para autenticación.
    /// Permite obtener un usuario por su username y password para validar credenciales.
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        private readonly ApiColombiaContext _context;

        public AuthRepository(ApiColombiaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene un usuario por username y password.
        /// Devuelve null si las credenciales no coinciden.
        /// </summary>
        /// <param name="username">Nombre de usuario</param>
        /// <param name="password">Password en hash</param>
        /// <param name="cancellationToken">Token para cancelar la operación</param>
        public async Task<User?> GetByUsernameAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            return await _context.User
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == password, cancellationToken);
        }
    }
}
