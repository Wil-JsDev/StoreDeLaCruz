using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreDeLaCruz.Core.Aplication.Interfaces.Service;
using StoreDeLaCruz.Core.Domain.Settings;
using StoreDeLaCruz.Insfraestructura.Shared.Service;

namespace StoreDeLaCruz.Insfraestructura.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfraestura(this IServiceCollection services, IConfiguration configuration)
        {
            #region Service
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            #endregion
        }
    }
}
