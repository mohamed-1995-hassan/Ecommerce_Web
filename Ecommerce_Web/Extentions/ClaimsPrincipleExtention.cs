using System.Security.Claims;

namespace Ecommerce_Web.Extentions
{
    public static class ClaimsPrincipleExtention
    {
        public static string RetrieveEmailFromPrincipl(this ClaimsPrincipal user)
        {
            return user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }
    }
}
