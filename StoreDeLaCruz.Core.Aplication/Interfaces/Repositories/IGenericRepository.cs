using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Core.Aplication.Interfaces.Repositories
{
    public interface IGenericRepository <TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(int id);

        Task Add(TEntity entity);

        void Update (TEntity entity);

        void Delete (TEntity entity);

        Task Save();
    }
}
