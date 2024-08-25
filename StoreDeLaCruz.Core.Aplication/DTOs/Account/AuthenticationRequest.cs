using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Core.Aplication.DTOs.Account
{
    public class AuthenticationRequest
    {
        //Estas dos prop son las que necesito para el logueo
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
