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
        public DateTime TF_FechaDeModificacion { get; set; }

        [Required]
        public int TN_IdUsuario { get; set; }

        public string TC_NombreUsuario { get; set; }

        [Required]
        public int TN_IdEstado { get; set; }

        public string TC_NombreEstado { get; set; }

        public int? TN_IdAnalisis { get; set; }

        public int? TN_IdSolicitud { get; set; }

    }

}
