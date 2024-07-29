using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Core.Aplication.DTOs.Nota
{
    public class NotaDTos
    {
        public int ID { get; set; }

        public int FolderId { get; set; }

        public string Titulo {  get; set; }

        public string Contenido { get; set; }

        public string PrioridadTarea { get; set; }

        public DateTime FechaDeCreacion { get; set; }
    }
}
