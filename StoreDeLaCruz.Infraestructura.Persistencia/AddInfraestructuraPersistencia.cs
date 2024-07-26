using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreDeLaCruz.Infraestructura.Persistencia.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Infraestructura.Persistencia
{
    public static class AddInfraestructuraPersistencia
    {
        public static void AddInfraestructura(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDeLaCruzContext>(p =>
            {
                p.UseSqlServer(configuration.GetConnectionString("StoreConnection"));
            });

            //Se debe de hacer la inyeccion de dependencia aqui desde Application a esta capa

        }

    }
}
