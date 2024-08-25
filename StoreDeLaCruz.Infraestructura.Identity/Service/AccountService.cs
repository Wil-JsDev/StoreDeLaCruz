using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using StoreDeLaCruz.Core.Aplication.DTOs.Account;
using StoreDeLaCruz.Core.Aplication.DTOs.Email;
using StoreDeLaCruz.Core.Aplication.Interfaces.Service;
using StoreDeLaCruz.Core.Domain.Enum;
using StoreDeLaCruz.Core.Domain.Settings;
using StoreDeLaCruz.Infraestructura.Identity.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace StoreDeLaCruz.Infraestructura.Identity.Service
{
    public class AccountService : IAccountService
    {
        private UserManager<ApplicationUser> _userManager; //Para crear usuarios
        private SignInManager<ApplicationUser> _signInManager; //Hace los logins 
        private IEmailService _emailService;
        private JWTSettings _jWTSettings;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IEmailService emailService, IOptions<JWTSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _jWTSettings = jwtSettings.Value;
        }

        //Este metodo sirve para autenticar o logueo, tendra mejoras con el JWT
        public async Task<AuthenticationResponse> AuthenticationAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new AuthenticationResponse();

            //Primero buscamos por su email, que es una forma de logueo de usuario .
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No account registered with {request.Email}";
                return response;
            }

            //Para el logueo con el Password, tecer parametro para recordar la cuenta del usuario
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.Email}";
                return response;
            }
            //Si no tiene el Email confirmado se lanzara un error
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account no confirmed for {request.Email}";
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            response.Id = user.Id;
            response.Username = user.UserName;
            response.Email = user.Email;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            response.JWTToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken); //Serializamos el token para poder retornar el token encriptado como tal
            var refresToken = GenerateRefreshToken();
            response.RefreshToken = refresToken.Token;

            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        //Esto solo funciona para registrar usuarios basicos
        public async Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin)
        {
            //Debemos validar que no tenga el mismo username y Email.
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUsername = await _userManager.FindByNameAsync(request.Username);
            if (userWithSameUsername != null)
            {
                response.HasError = true;
                response.Error = $"usernme '{request.Username}' is already taken";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email {request.Email} is already registered";
                return response;
            }

            //Se crea el usuario
            var user = new ApplicationUser()
            {
                Email = request.Email,
                UserName = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                var verification = await SendVerificationEmilUrl(user, origin);
                await _emailService.SenAsync(new EmailRequestDto
                {
                    To = user.Email,
                    Body = $"Please confirm your account visiting this url {verification}",
                    Subject = "Confirm registration"
                });

            }
            else
            {
                response.HasError = true;
                response.Error = "An error ocurred trying to registed the user";
                return response;
            }

            return response;
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return $"No Account registered with this user";
            }

            //Decodamos el token
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            //Confirmamos la cuenta
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirm for {user.Email}. you can now use the app";
            }
            else
            {
                return $"An error occurred while confirming{user.Email}";
            }

        }


        #region Private Method
        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            //Obtenemos los claims, los claims son los permisos
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

           foreach (var role in roles)
           {
                roleClaims.Add(new Claim("roles", role));
           }

            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName), //El dueño de ese token (sub)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //Es un identificador unico, es como el ID del token
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id) //Claim personalizado
            }
            .Union(userClaims)
            .Union(roleClaims);

            //Generar llave simetrica de seguridad
            var symmectricSecutityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.Key));
            var signingCredetials = new SigningCredentials(symmectricSecutityKey, SecurityAlgorithms.HmacSha256);

            //Este es el token ya creado
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jWTSettings.Issuer,
                audience: _jWTSettings.Audience,
                claims: claim,
                expires: DateTime.UtcNow.AddMinutes(_jWTSettings.DurationInMinutes),
                signingCredentials: signingCredetials
                );

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expired = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomByte = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomByte);

            return BitConverter.ToString(randomByte).Replace("-","");
        }

        //Para armar la URI, para enviarla a mi correo para la confirmacion
        private async Task<string> SendVerificationEmilUrl(ApplicationUser user, string origin)
        {
            //Origin es el localhost desde donde se ejectua el API, eso se obtiene desde el controller, porque puede variar
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user); //Para crear un token de validaciones con Identity
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code)); //Esto es para condificarlo el token generado por Identity

            var route = "User/ConfirmEmail"; //
            var Uri = new Uri(string.Concat($"{origin}/", route));

            //Al Uri le voy añadir esos parametros, eso se se hace con estas funciones  QueryHelpers.AddQueryString(), user.Id es el valor
            var verificationUrl = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUrl = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);

            return verificationUrl;
        }

        #endregion
    }
}
