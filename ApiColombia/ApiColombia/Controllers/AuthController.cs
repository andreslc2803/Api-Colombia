using ApiColombia.BL.AuthService.Interfaces;
using ApiColombia.Entities.DTO.Security;
using ApiColombia.Security;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace ApiColombia.Controllers
{
    /// <summary>
    /// Controlador encargado de gestionar la autenticación de usuarios y la generación de tokens JWT para el acceso a los endpoints protegidos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;

        public AuthController(ITokenService tokenService, IAuthService authService)
        {
            _tokenService = tokenService;
            _authService = authService;
        }

        /// <summary>
        /// Autentica un usuario y retorna un token JWT.
        /// </summary>
        /// <param name="request">Credenciales del usuario.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Token JWT si la autenticación es exitosa.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.LoginAsync(
                request.Username,
                request.Password,
                cancellationToken);

            return Ok(new { token });
        }
    }
}