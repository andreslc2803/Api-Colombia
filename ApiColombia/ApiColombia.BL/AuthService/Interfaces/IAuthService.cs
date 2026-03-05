using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiColombia.BL.AuthService.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string username, string password, CancellationToken cancellationToken = default);
    }
}
