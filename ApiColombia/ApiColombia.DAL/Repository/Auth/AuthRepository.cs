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
    public class AuthRepository : IAuthRepository
    {
        private readonly ApiColombiaContext _context;

        public AuthRepository(ApiColombiaContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            return await _context.User
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == password, cancellationToken);
        }
    }
}
