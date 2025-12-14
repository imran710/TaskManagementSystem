using System.Security.Claims;

namespace TaskManagement.Application.Helper;
public static class UserClaimGenerator
{
    public const string RoleSeparator = "~";

    public static class CustomClaimTypes
    {
        public const string UserId = "UserId";
        public const string UserName = "UserName";
        public const string Role = "Role";
        public const string SubscriptionPlan = "SubscriptionPlan";
    }

    public static IEnumerable<Claim> GetUserClaims(long userId,string username)
    {
        var claims = new List<Claim>
        {
            new(CustomClaimTypes.UserId, value: userId.ToString(provider: null)),
            new(CustomClaimTypes.UserName, value: username), 
        };
        return claims;
    }
 

    public static IEnumerable<string> StringToRoleNames(string roleString)
    {
        return roleString.Split(RoleSeparator);
    }
}

