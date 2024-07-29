using AutoMapper;
using StoreDeLaCruz.Core.Aplication.DTOs.Folder;
using StoreDeLaCruz.Core.Aplication.DTOs.Nota;
using StoreDeLaCruz.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Core.Aplication.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region NotaDTos
            CreateMap<NotaInsertDTos, Nota>();

            //Con datos diferentes
            CreateMap<Nota, NotaDTos>()
                .ForMember(dto => dto.ID
                , m => m.MapFrom(n => n.NotaId));
                
            CreateMap<NotaUpdateDTos, Nota>();

            #endregion

            #region FolderDtos
            CreateMap<FolderInsertDTos, Folder>();
            CreateMap<Folder, FolderDTos>()
                .ForMember(dto => dto.Id,
                m => m.MapFrom(n => n.FolderId));
            CreateMap<FolderUpdate, Folder>();
            #endregion
        }
    }
}
