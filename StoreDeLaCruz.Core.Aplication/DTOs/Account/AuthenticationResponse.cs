using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Core.Aplication.DTOs.Account
{
    public class AuthenticationResponse
    {
        //Esta clase es para devolver los datos a los usuarios
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public List<string> Roles { get; set; }

        public bool IsVerified { get; set; }

        public bool HasError { get; set; }

        public string Error { get; set; }

        public string JWTToken { get; set; }

        [JsonIgnore] //Esta propiedad sera ignorada dentro del WEB API
        public string RefreshToken { get; set; }
    }
}
