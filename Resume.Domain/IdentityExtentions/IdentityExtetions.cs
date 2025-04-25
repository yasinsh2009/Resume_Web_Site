using System.Security.Claims;
using System.Security.Principal;

namespace Resume.Domain.IdentityExtentions
{
    public static class IdentityExtetions
    {
        public static long GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                return default;
            }

            string? userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return string.IsNullOrWhiteSpace(userId) ? default : long.Parse(userId);
        }

        public static long GetUserId(this IPrincipal principal)
        {
            if (principal == null)
            {
                return default;
            }

            var user = (ClaimsPrincipal)principal;

            return user.GetUserId();
        }
    }
}
