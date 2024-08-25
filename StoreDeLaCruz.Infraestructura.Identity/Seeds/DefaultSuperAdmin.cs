using Microsoft.AspNetCore.Identity;
using StoreDeLaCruz.Core.Domain.Enum;
using StoreDeLaCruz.Infraestructura.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Infraestructura.Identity.Seeds
{
    public static class DefaultSuperAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Creamos el usuario por default
            ApplicationUser defaultUSer = new ApplicationUser()
            {
                UserName = "superAdminUser",
                Email = "wilmerjose12@gmail.com",
                FirstName = "Wilmer",
                LastName = "Jose",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            //Validamos si el usuario existe, para no crearlo cada vez cuando el software se ejecute
            if (userManager.Users.All(u => u.Id != defaultUSer.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUSer.Email);

                if (user == null)
                {
                    await userManager.CreateAsync(defaultUSer, "1234Pa$$word");
                    await userManager.AddToRoleAsync(defaultUSer, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUSer, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUSer, Roles.SuperAdmin.ToString());
                }
            }
          
        }
    }
}
