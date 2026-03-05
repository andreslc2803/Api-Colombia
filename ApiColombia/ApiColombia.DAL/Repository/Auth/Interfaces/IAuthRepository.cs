using ApiColombia.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiColombia.DAL.Repository.Auth.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetByUsernameAsync(string username, string password, CancellationToken cancellationToken = default);
    }
}
