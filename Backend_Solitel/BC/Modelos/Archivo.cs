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

        [Required]
        public string Nombre { get; set; }

        [Required]
        public byte[] Contenido { get; set; } // Para almacenar contenido binario

        [Required]
        [StringLength(50)]
        public string FormatoArchivo { get; set; }

        [Required]
        public DateTime FechaModificacion { get; set; }


    }

}
