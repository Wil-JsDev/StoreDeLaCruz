using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StoreDeLaCruz.Core.Aplication.DTOs.Account;
using StoreDeLaCruz.Core.Aplication.Interfaces.Service;
using StoreDeLaCruz.Core.Domain.Settings;
using StoreDeLaCruz.Infraestructura.Identity.Context;
using StoreDeLaCruz.Infraestructura.Identity.Entities;
using StoreDeLaCruz.Infraestructura.Identity.Service;
using System.Text;

namespace StoreDeLaCruz.Infraestructura.Identity
{
    public static class AddInfraestructuraIdentity
    {
        public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {

            #region Context
            services.AddDbContext<IdentityContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("IdentityTiendaDeLaCruz"),
            b => b.MigrationsAssembly("StoreDeLaCruz.Infraestructura.Identity")));
            #endregion

            //Se debe de hacer la inyeccion de dependencia aqui desde Application a esta capa
            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>()
                                                                 .AddDefaultTokenProviders();

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                //Esto es para validar los parametros que trae el token
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero, //Sirve para la validacion del tiempo, para que no de tiempo extra.
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))

                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        //Cuando el codigo explote, porque algo no se manejo
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = c =>
                    {
                        //No estas autorizado o fue un token invalido 
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You are not Authorized" });
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {
                        //Es para cuando el usuario no tiene permiso para entrar
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You are not Authorized to access this resource" });
                        return c.Response.WriteAsync(result);

                    }
                };

            });
            #endregion

            #region Services

            services.AddScoped<IAccountService, AccountService>();

            #endregion

        }

    }
}
