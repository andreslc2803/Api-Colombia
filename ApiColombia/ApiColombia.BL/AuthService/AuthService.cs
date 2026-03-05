using ApiColombia.BL.AuthService.Interfaces;
using ApiColombia.BL.Exceptions;
using ApiColombia.DAL.Data;
using ApiColombia.DAL.Repository.Auth.Interfaces;
using ApiColombia.Entities.Entities;
using ApiColombia.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiColombia.BL.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepo;
        private readonly ITokenService _tokenService;

        public AuthService(IAuthRepository authRepo, ITokenService tokenService)
        {
            _authRepo = authRepo;
            _tokenService = tokenService;
        }

        public async Task<string> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            var user = await _authRepo.GetByUsernameAsync(username, password, cancellationToken);

            if (user == null)
                throw new BusinessException("Credenciales inválidas");

            if (user.PasswordHash != password)
                throw new BusinessException("Credenciales inválidas");

            return _tokenService.GenerateToken(user.Username);
        }
    }
}
