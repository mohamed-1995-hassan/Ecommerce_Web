
using Core.Entities.identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data.Seeding
{
    public class AppIdentitySeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> usermanager)
        {
            if (!usermanager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Mohamed",
                    Email = "mohamed25hassan67@gmail.com",
                    UserName = "mohamed25hassan67@gmail.com",
                    Address = new Address
                    {
                        FirstName = "Masr",
                        LastName = "Gomhoraia",
                        City = "Domyat",
                        State = "Eg",
                        Street = "ElShall Street",
                        Zip = "12345"
                    }
                };
                await usermanager.CreateAsync(user,"P@ssw0rd");
            }
        }
    }
}
