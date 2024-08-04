using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Core.Aplication.Interfaces.Service
{
    public interface IGenericService <T, TI, TU>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task<T> Add(TI EntityInsert);

        Task<T> Update(int id,TU Entity);

        Task<T> Delete(int id);
    }
}
