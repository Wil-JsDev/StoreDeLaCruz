using StoreDeLaCruz.Core.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Core.Domain.Entities
{
    public class Nota
    {
        public int NotaId { get; set; }
        
        public int FolderId { get; set; }

        public string Titulo {  get; set; }

        public string Contenido { get; set; }

        public Folder Folder { get; set; }

        public Prioridad PrioridadTarea { get; set; }

        public DateTime FechaDeCreacion { get; set; }
    }
}
