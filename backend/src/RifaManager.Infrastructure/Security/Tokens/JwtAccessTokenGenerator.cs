using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Security.Tokens;

namespace RifaManager.Infrastructure.Security.Tokens;

internal sealed class JwtAccessTokenGenerator(IConfiguration configuration) : IAccessTokenGenerator
{
    public string Generate(Usuario usuario)
    {
        string secretKey = configuration["Jwt:SecretKey"]
            ?? throw new InvalidOperationException("JWT secret key not configured.");

        string issuer = configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException("JWT issuer not configured.");

        string audience = configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException("JWT audience not configured.");

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(secretKey));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims =
        [
            new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, usuario.Email),
            new(ClaimTypes.Name, usuario.Nome),
            new(ClaimTypes.Role, usuario.Perfil.ToString())
        ];

        JwtSecurityToken token = new
        (
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
