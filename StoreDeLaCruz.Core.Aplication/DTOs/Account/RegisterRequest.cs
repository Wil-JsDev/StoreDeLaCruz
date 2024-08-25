using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Core.Aplication.DTOs.Account
{
    public class RegisterRequest
    {
        //Los datos que necesitamos para registrar el usuario
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmEmail { get; set; }

        public string Phone { get; set; }
    }
}
