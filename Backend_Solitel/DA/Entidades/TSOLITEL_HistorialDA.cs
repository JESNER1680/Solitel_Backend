using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_Historial")]
    public class TSOLITEL_HistorialDA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TN_IdHistorial { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_Observacion { get; set; }

        [Required]
        public DateTime TF_FechaEstado { get; set; }

        [Required]
        public int TN_IdUsuario { get; set; }

        [Required]
        public int TN_IdEstado { get; set; }

        public int? TN_IdAnalisis { get; set; }

        public int? TN_NumeroSolicitud { get; set; }

        [ForeignKey("TN_IdUsuario")]
        public virtual TSOLITEL_UsuarioDA Usuario { get; set; }

        [ForeignKey("TN_IdEstado")]
        public virtual TSOLITEL_EstadoDA Estado { get; set; }

        [ForeignKey("TN_NumeroSolicitud")]
        public virtual TSOLITEL_SolicitudProveedorDA? SolicitudProveedor { get; set; }
    }

}
