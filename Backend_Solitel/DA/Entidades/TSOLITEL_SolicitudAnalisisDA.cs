using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_SolicitudAnalisis")]
    public class TSOLITEL_SolicitudAnalisisDA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TN_IdSolicitudAnalisis { get; set; }

        [Required]
        public DateTime TF_FechaDelHecho { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_OtrosDetalles { get; set; }

        [StringLength(255)]
        public string? TC_OtrosObjetivosDeAnalisis { get; set; }

        [Required]
        public bool TB_Aprobado { get; set; }

        public DateTime? TF_FechaCrecion { get; set; }

        [Required]
        public int TN_NumeroSolicitud { get; set; }

        [Required]
        public int TN_IdOficina { get; set; }

        [ForeignKey("TN_NumeroSolicitud")]
        public virtual TSOLITEL_SolicitudProveedorDA SolicitudProveedor { get; set; }

        [ForeignKey("TN_IdOficina")]
        public virtual TSOLITEL_OficinaDA Oficina { get; set; }
    }

}
