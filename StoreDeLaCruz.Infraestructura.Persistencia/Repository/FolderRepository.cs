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
    public class FolderRepository : ICommonRepository<Folder>
    {
        private StoreDeLaCruzContext _context;

        public FolderRepository(StoreDeLaCruzContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Folder>> GetAll() =>
            await _context.Folders.ToListAsync();

        public async Task<Folder> GetById(int id) =>
            await _context.Folders.FindAsync(id);

        public async Task Add(Folder folder) =>
            await _context.Folders.AddAsync(folder);

        public void Update(Folder entity)
        {
            _context.Folders.Attach(entity);
            _context.Folders.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Folder entity)
        {
            _context.Folders.Remove(entity);
        }

        public async Task Save() =>
            await _context.SaveChangesAsync();        

    }
}
