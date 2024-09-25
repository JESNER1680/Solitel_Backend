using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_RequerimientoProveedor")]
    public class TSOLITEL_RequerimientoProveedorDA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TN_IdRequerimientoProveedor { get; set; }

        [Required]
        public DateTime TF_FechaInicio { get; set; }

        [Required]
        public DateTime TF_FechaFinal { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_Requerimiento { get; set; }

        [Required]
        public int TN_NumeroSolicitud { get; set; }

        [ForeignKey("TN_NumeroSolicitud")]
        public virtual TSOLITEL_SolicitudProveedorDA SolicitudProveedor { get; set; }
    }

}
