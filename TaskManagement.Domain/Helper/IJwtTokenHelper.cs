using System.Security.Claims;


namespace TaskManagement.Domain.Helper;

public interface IJwtTokenHelper
{
    JwtOption JwtOption { get; }
    string GenerateAccessToken(IEnumerable<Claim> claims);
}
