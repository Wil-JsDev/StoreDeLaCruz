using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Core.Aplication.Interfaces.Service
{
    public interface INoteService<T, TI, TU> : IGenericService<T, TI, TU>
    {
        Task<IEnumerable<T>> Filter(string filter);
    }
}
