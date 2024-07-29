using Microsoft.Extensions.DependencyInjection;
using StoreDeLaCruz.Core.Aplication.DTOs.Folder;
using StoreDeLaCruz.Core.Aplication.DTOs.Nota;
using StoreDeLaCruz.Core.Aplication.Interfaces.Service;
using StoreDeLaCruz.Core.Aplication.Mapping;
using StoreDeLaCruz.Core.Aplication.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Core.Aplication
{
    public static class AddApplicationCore
    {
        public static void AddApplication(this IServiceCollection services)
        {

            #region Services
            //Aqui van todas las inyecciones de dependencia de los service, es decir, las interfaces e inplementaciones de esas interfaces
            services.AddScoped<ICommonService<NotaDTos,NotaInsertDTos,NotaUpdateDTos>, NoteService>();
            services.AddScoped<IFolderService<FolderDTos, FolderInsertDTos,FolderUpdate>, FolderService>();
            #endregion

            #region Mapper
            services.AddAutoMapper(typeof(MappingProfile));
            #endregion
        }

    }
}
