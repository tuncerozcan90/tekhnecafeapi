using System.Security.Claims;

namespace TekhneCafe.Business.Extensions
{
    public static class GetActiveUser
    {
        public static string ActiveUserId(this ClaimsPrincipal claims)
        {
            var userClaims = claims.Identities.First(_ => _.IsAuthenticated);
            return userClaims?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string ActiveUserName(this ClaimsPrincipal claims)
        {
            var userClaims = claims.Identities.First(_ => _.IsAuthenticated);
            return userClaims?.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}
