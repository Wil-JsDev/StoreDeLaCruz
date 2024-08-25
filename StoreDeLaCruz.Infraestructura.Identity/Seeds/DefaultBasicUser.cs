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
    public static class DefaultBasicUser
    {
        
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Creamos el usuario por default
            ApplicationUser defaultUSer = new()
            {
                UserName = "basicUser",
                Email = "basicuser@gmail.com",
                FirstName = "John",
                LastName = "Perez",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            //Validamos si el usuario existe, para no crearlo cada vez cuando el software se ejecute
            if (userManager.Users.All(user => user.Id != defaultUSer.Id))
            {
                var userEmail = await userManager.FindByEmailAsync(defaultUSer.Email);

                if (userEmail == null)
                {
                    await userManager.CreateAsync(defaultUSer, "123Pa$$word");
                    await userManager.AddToRoleAsync(defaultUSer, Roles.Basic.ToString());
                }

            }
           
        }

    }
}
