using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace TodoAPI.Services;

public class TokenService : ITokenService
{
    public JwtSecurityToken GenerateAcessToken(IEnumerable<Claim> claims, IConfiguration _config)
    {
        string key = _config.GetSection("JWT").GetValue<String>("SecretKey") ?? throw new InvalidOperationException("Invalid Secret key");
        byte[] secretKey = Encoding.UTF8.GetBytes(key);

        SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        SecurityTokenDescriptor securityTokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_config.GetSection("JWT").GetValue<double>("TokenValidityInMinutes")),
            Audience = _config.GetSection("JWT").GetValue<string>("ValidAudience"),
            Issuer = _config.GetSection("JWT").GetValue<string>("ValidIssuer"),
            SigningCredentials = signingCredentials
        };

        JwtSecurityTokenHandler tokenHandler = new();
        JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(securityTokenDescriptor);

        return token;
    }

    public string GenerateRefreshToken()
    {
        byte[] secureRandomBytes = new byte[128];

        using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

        randomNumberGenerator.GetBytes(secureRandomBytes);

        string refreshToken = Convert.ToBase64String(secureRandomBytes);

        return refreshToken;
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config)
    {
        string key = _config["JWT:SecretKey"] ?? throw new InvalidOperationException("Invalid key");

        TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidateLifetime = false
        };

        JwtSecurityTokenHandler tokenHandler = new();
        ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid Token");
        }

        return principal;
    }
}
