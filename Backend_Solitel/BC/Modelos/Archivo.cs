using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Modelos
{
    public class Archivo
    {
        public int IdArchivo { get; set; }

        public string Nombre { get; set; }

        public byte[] Contenido { get; set; }

        public string FormatoArchivo { get; set; }

        public DateTime FechaModificacion { get; set; }

    }

}
