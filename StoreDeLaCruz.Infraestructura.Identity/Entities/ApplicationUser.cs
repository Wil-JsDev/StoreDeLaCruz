using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Infraestructura.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        //Aqui extendemos la funcionalidad de IdentityUser, usamos la clase ApplicationUser para usarla en el context
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
