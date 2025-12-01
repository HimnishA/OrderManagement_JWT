using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using OrderManagementApi.Models;


namespace OrderManagementApi.Services;


public class JwtSettings
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int ExpiresMinutes { get; set; }
}


public class TokenService : ITokenService
{
    private readonly JwtSettings _settings;


    public TokenService(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
    }


    public string CreateToken(ApplicationUser user)
    {
        var claims = new List<Claim>
{
new Claim(JwtRegisteredClaimNames.Sub, user.Id),
new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
new Claim(ClaimTypes.NameIdentifier, user.Id),
new Claim(ClaimTypes.Name, user.UserName ?? user.Email ?? string.Empty)
};


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(_settings.ExpiresMinutes);


        var token = new JwtSecurityToken(
        issuer: _settings.Issuer,
        audience: _settings.Audience,
        claims: claims,
        expires: expires,
        signingCredentials: creds
        );


        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}