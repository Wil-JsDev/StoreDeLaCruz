using AutoMapper;
using StoreDeLaCruz.Core.Aplication.DTOs.Nota;
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
    public class NoteService : INoteService<NotaDTos, NotaInsertDTos, NotaUpdateDTos>
    {
        private INoteRepository <Nota> _noteRepository;
        private IMapper _mapper;

        public NoteService(INoteRepository<Nota> noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotaDTos>> GetAll()
        {
            var note = await _noteRepository.GetAll();

            return note.Select(b => _mapper.Map<NotaDTos>(b));

        }

        public async Task<NotaDTos> GetById(int id)
        {
            var noteId = await _noteRepository.GetById(id);

            if (noteId != null)
            {
                var noteDtos = _mapper.Map<NotaDTos>(noteId);

                return noteDtos;
            }

            return null;
        }

        public async Task<NotaDTos> Add(NotaInsertDTos EntityInsert)
        {
            var note = _mapper.Map<Nota>(EntityInsert);
            await _noteRepository.Add(note);
            await _noteRepository.Save();

            var noteDto = _mapper.Map<NotaDTos>(note);

            return noteDto;

        }

        public async Task<NotaDTos> Delete(int id)
        {
            var noteID = await _noteRepository.GetById(id);

            if (noteID != null)
            {
                  noteID = _mapper.Map<Nota>(noteID);
                 _noteRepository.Delete(noteID);
                 await _noteRepository.Save();
                 var noteDto = _mapper.Map<NotaDTos>(noteID);

                return noteDto;
            }

            return null;
        }

        public async Task<NotaDTos> Update(int id, NotaUpdateDTos Entity)
        {
            var note = await _noteRepository.GetById(id);

            if (note != null)
            {
                note = _mapper.Map<NotaUpdateDTos, Nota>(Entity, note);
                _noteRepository.Update(note);
                await _noteRepository.Save();

                var noteDto = _mapper.Map<NotaDTos>(note);
                return noteDto;
            }
            return null;
        }

        public async Task<IEnumerable<NotaDTos>> Filter(string filter)
        {
            var resultFilter = await _noteRepository.Search(filter);
            
            if (resultFilter != null)
            {
                return resultFilter.Select(b => _mapper.Map<NotaDTos>(b));
            }

            return  null;
        }
    }
}
