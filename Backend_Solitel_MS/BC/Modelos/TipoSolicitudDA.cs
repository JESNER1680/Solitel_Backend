using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Modelos
{
    [Table("TSOLITEL_TipoSolicitud")]
    public class TipoSolicitudDA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TN_IdTipoSolicitud { get; set; }

        [Required]
        [StringLength(50)]
        public string TC_Nombre { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_Descripcion { get; set; }
    }

}
