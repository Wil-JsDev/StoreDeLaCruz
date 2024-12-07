using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreDeLaCruz.Core.Aplication.Interfaces.Repositories;
using StoreDeLaCruz.Core.Domain.Entities;
using StoreDeLaCruz.Infraestructura.Persistencia.Context;
using StoreDeLaCruz.Infraestructura.Persistencia.Repository;
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
                p.UseNpgsql(configuration.GetConnectionString("TiendaDeLaCruz"));
            });

            //Se debe de hacer la inyeccion de dependencia aqui desde Application a esta capa
            #region Repository
            services.AddScoped<INoteRepository<Nota>, NoteRepository>();
            services.AddScoped<IFolderRepository<Folder>, FolderRepository>();
            #endregion

        }

    }
}
