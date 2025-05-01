using Core.Entities.identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecommerce_Web.Extentions
{
    public static class UserManagerExtentions
    {
        public static async Task<AppUser> FindUserByClaimsPrincipleWithAddress(this UserManager<AppUser> userManager,
                                                                              ClaimsPrincipal user)
        {
            var email = user.FindFirst(ClaimTypes.Email);
            return await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(x => x.Email == email.Value);
        }
        public static async Task<AppUser> FindByEmailFromClaimsPrinciple(this UserManager<AppUser> userManager,
                                                                         ClaimsPrincipal user)
        {
            var email = user.FindFirst(ClaimTypes.Email);
            return await userManager.Users.FirstOrDefaultAsync(x => x.Email == email.Value);
        }
    }
}
