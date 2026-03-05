using ApiColombia.Entities.DTO.Security;
using ApiColombia.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiColombia.Security
{
    public interface ITokenService
    {
        string GenerateToken(string username);
    }

    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Servicio encargado de generar tokens JWT para usuarios autenticados.
        /// - Implementa ITokenService.
        /// - Usa la configuración JwtSettings para issuer, audience, clave y duración.
        /// - Incluye claims básicos: nombre de usuario y JTI (identificador único del token).
        /// - Retorna el token JWT en formato string listo para usarse en autorizaciones.
        /// </summary>
        public string GenerateToken(string username)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}