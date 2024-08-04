using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Core.Aplication.Interfaces.Service
{
    public interface IFolderService <T, TI, TU> : IGenericService<T, TI, TU>
    {
        Task Search(string search);
    }
}
