using System.Linq;
using System.Security.Claims;

namespace Oqtane.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Username(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.HasClaim(item => item.Type == ClaimTypes.Name))
            {
                return claimsPrincipal.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name).Value;
            }
            else
            {
                return "";
            }
        }

        public static int UserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.HasClaim(item => item.Type == ClaimTypes.NameIdentifier))
            {
                return int.Parse(claimsPrincipal.Claims.First(item => item.Type == ClaimTypes.NameIdentifier).Value);
            }
            else
            {
                return -1;
            }
        }

        public static string Roles(this ClaimsPrincipal claimsPrincipal)
        {
            var roles = "";
            foreach (var claim in claimsPrincipal.Claims.Where(item => item.Type == ClaimTypes.Role))
            {
                roles += ((roles == "") ? "" : ";") + claim.Value;
            }
            return roles;
        }

        public static string SiteKey(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.HasClaim(item => item.Type == "sitekey"))
            {
                return claimsPrincipal.Claims.FirstOrDefault(item => item.Type == "sitekey").Value;
            }
            else
            {
                return "";
            }
        }

        public static int TenantId(this ClaimsPrincipal claimsPrincipal)
        {
            var sitekey = SiteKey(claimsPrincipal);
            if (!string.IsNullOrEmpty(sitekey) && sitekey.Contains(":"))
            {
                return int.Parse(sitekey.Split(':')[0]);
            }
            return -1;
        }

        public static int SiteId(this ClaimsPrincipal claimsPrincipal)
        {
            var sitekey = SiteKey(claimsPrincipal);
            if (!string.IsNullOrEmpty(sitekey) && sitekey.Contains(":"))
            {
                return int.Parse(sitekey.Split(':')[1]);
            }
            return -1;
        }
    }
}
