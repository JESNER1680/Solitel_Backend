using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    public class TSOLITEL_ArchivoDA
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TN_IdArchivo { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_HashAchivo { get; set; }

        [Required]
        public int TC_Nombre { get; set; }

        [Required]
        public byte[] TV_Contenido { get; set; }

        [Required]
        [StringLength(50)]
        public string TC_FormatoAchivo { get; set; }

        [Required]
        public DateTime TF_FechaModificacion { get; set; }

        public int? TN_IdRequerimiento { get; set; }

        // Relación opcional con la entidad RequerimientoProveedor
        [ForeignKey("TN_IdRequerimiento")]
        public virtual TSOLITEL_RequerimientoProveedorDA? RequerimientoProveedor { get; set; }
    }
}
