﻿using StoreDeLaCruz.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Core.Aplication.Interfaces.Repositories
{
    public interface INoteRepository<TEntity> : IGenericRepository<TEntity>
    {  
       Task<IEnumerable<TEntity>> Search(string filter);
    }
}
