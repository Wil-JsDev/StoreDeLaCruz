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
    public static class DefaultRoles
    {
        //UserManager<> sirve para crear usuarios, update, recordar pass y Tokens
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Estos serian los roles por default que tendriamos
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));

        }

    }
}
