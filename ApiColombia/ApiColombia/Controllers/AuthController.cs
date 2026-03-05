using ApiColombia.BL.AuthService.Interfaces;
using ApiColombia.Security;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace ApiColombia.Controllers
{
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken)
        {
            var token = await _authService.LoginAsync(
                request.Username,
                request.Password,
                cancellationToken);

            return Ok(new { token });
        }
    }

    public record LoginRequest(string Username, string Password);
}