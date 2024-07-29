using Microsoft.EntityFrameworkCore;
using StoreDeLaCruz.Core.Aplication.Interfaces.Repositories;
using StoreDeLaCruz.Core.Domain.Entities;
using StoreDeLaCruz.Infraestructura.Persistencia.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Infraestructura.Persistencia.Repository
{
    public class NoteRepository : INoteRepository<Nota>
    {
        private StoreDeLaCruzContext _context;

        public NoteRepository(StoreDeLaCruzContext context)
        {
            _context = context; 
        }

        public async Task Add(Nota entity)
        {
            await _context.Notas.AddAsync(entity);
        }

        public async Task<IEnumerable<Nota>> GetAll() =>
            await _context.Notas.ToListAsync();

        public async Task<Nota> GetById(int id) =>
            await _context.Notas.FindAsync(id);

        public void Update(Nota entity)
        {
            _context.Notas.Attach(entity);
            _context.Notas.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(Nota entity)
        {
            _context.Notas.Remove(entity);
        }

       public IEnumerable<Nota> Search(Func<Nota, bool> filter) =>
             _context.Notas.Where(filter).ToList();

       public async Task Save() =>
            await _context.SaveChangesAsync();

    }
}
