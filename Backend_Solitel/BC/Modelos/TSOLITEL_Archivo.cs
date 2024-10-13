using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Modelos
{
    public class TSOLITEL_Archivo
    {
        public int TN_IdArchivo { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_HashAchivo { get; set; }

        [Required]
        public int TC_Nombre { get; set; }

        [Required]
        public byte[] TV_Contenido { get; set; } // Para almacenar contenido binario

        [Required]
        [StringLength(50)]
        public string TC_FormatoAchivo { get; set; }

        [Required]
        public DateTime TF_FechaModificacion { get; set; }

        // Relación opcional con TSOLITEL_RequerimientoProveedor
        public int? TN_IdRequerimiento { get; set; }
        public virtual TSOLITEL_RequerimientoProveedor RequerimientoProveedor { get; set; }
    }

}
