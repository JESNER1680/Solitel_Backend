using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Modelos
{
    public class Asignacion
    {
        [Key]
        [Required]
        public int TN_IdUsuario { get; set; }

        public int? TN_IdAnalisis { get; set; }

        public int? TN_NumeroSolicitud { get; set; }

        [Required]
        public DateTime TF_FechaModificacion { get; set; }

        // Relaciones con otras entidades
        public virtual SolicitudAnalisis? SolicitudAnalisis { get; set; }
        public virtual SolicitudProveedor? SolicitudProveedor { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }


}
