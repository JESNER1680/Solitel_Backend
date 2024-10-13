using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_Asignacion")]
    public class TSOLITEL_AsignacionDA
    {
        [Key]
        [Required]
        public int TN_IdUsuario { get; set; }

        public int? TN_IdAnalisis { get; set; }

        public int? TN_NumeroSolicitud { get; set; }

        [Required]
        public DateTime TF_FechaModificacion { get; set; }

        [ForeignKey("TN_IdAnalisis")]
        public virtual TSOLITEL_SolicitudAnalisisDA? SolicitudAnalisis { get; set; }

        [ForeignKey("TN_NumeroSolicitud")]
        public virtual TSOLITEL_SolicitudProveedorDA? SolicitudProveedor { get; set; }

        [ForeignKey("TN_IdUsuario")]
        public virtual TSOLITEL_UsuarioDA? Usuario { get; set; }
    }


}
