using AutoMapper;
using StoreDeLaCruz.Core.Aplication.DTOs.Folder;
using StoreDeLaCruz.Core.Aplication.Interfaces.Repositories;
using StoreDeLaCruz.Core.Aplication.Interfaces.Service;
using StoreDeLaCruz.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Core.Aplication.Service
{
    public class FolderService : IFolderService<FolderDTos, FolderInsertDTos, FolderUpdate>
    {
        private IFolderRepository<Folder> _folderRepository;
        private IMapper _mapper;

        public FolderService(IFolderRepository<Folder>folderRepository,
            IMapper mapper)
        {
            _folderRepository = folderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FolderDTos>> GetAll()
        {
            var folder = await _folderRepository.GetAll();

            return folder.Select(f => _mapper.Map<FolderDTos>(f));
        }

        public async Task<FolderDTos> GetById(int id)
        {
            var folderId = await _folderRepository.GetById(id);

            if (folderId != null)
            {
                var folderDto = _mapper.Map<FolderDTos>(folderId);

                return folderDto;
            }

            return null;
        }

        public async Task<FolderDTos> Add(FolderInsertDTos insertFolder)
        {
            var folderInsert = _mapper.Map<Folder>(insertFolder);
            await _folderRepository.Add(folderInsert);
            await _folderRepository.Save();

            var folderDto = _mapper.Map<FolderDTos>(folderInsert);

            return folderDto;
        }

        public async Task<FolderDTos> Update(int id, FolderUpdate updateFolder)
        {
            var folder = await _folderRepository.GetById(id);

            if (folder != null)
            {
                folder = _mapper.Map<FolderUpdate, Folder>(updateFolder, folder);
                 _folderRepository.Update(folder);
                 await  _folderRepository.Save();

                var folderDto = _mapper.Map<FolderDTos>(folder);

                return folderDto;
            }

            return null;
            
        }

        public async Task<FolderDTos> Delete(int id)
        {
            var folder = await _folderRepository.GetById(id);

            if (folder != null)
            {
                
                folder = _mapper.Map<Folder>(folder);
                _folderRepository.Delete(folder);
                 await _folderRepository.Save();

                var folderDto = _mapper.Map<FolderDTos>(folder);

                return folderDto;
            }

            return null;

        }

    }
}
