using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Mayar Hany",
                    Email = "Mayar@gmail.com",
                    UserName = "mayarhany",
                    address = new Address
                    {
                        FirstName = "Mayar",
                        LastName = "Hany", 
                        City = "Mansoura",
                        State = "Dakahlia",
                        Street = "St",
                        PostalCode = "123456"

                    }
                };
                await userManager.CreateAsync(user, "password123!");
            }
        }
    }
}
