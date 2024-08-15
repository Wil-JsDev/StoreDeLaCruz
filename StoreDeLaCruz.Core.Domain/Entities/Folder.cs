using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Core.Domain.Entities
{
    public class Folder
    {
        public int FolderId {  get; set; }

        public string Name { get; set; }

        public ICollection<Nota> Notas { get; set; }
 
    }
}
