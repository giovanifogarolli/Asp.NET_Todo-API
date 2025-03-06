using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TodoAPI.Services;

public interface ITokenService
{
    JwtSecurityToken GenerateAcessToken(IEnumerable<Claim> claims, IConfiguration _config);

    String GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(String token, IConfiguration _config);
}
