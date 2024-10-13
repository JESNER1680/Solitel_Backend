using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_TipoAnalisis_SolicitudAnalisis")]
    public class TSOLITEL_TipoAnalisis_SolicitudAnalisisDA
    {
        [Key, Column(Order = 0)]
        [Required]
        public int TN_IdTipoAnalisis { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        public int TN_IdAnalisis { get; set; }
        [ForeignKey("TN_IdTipoAnalisis")]
        public virtual TSOLITEL_TipoAnalisisDA TipoAnalisis { get; set; }

        [ForeignKey("TN_IdAnalisis")]
        public virtual TSOLITEL_SolicitudAnalisisDA SolicitudAnalisis { get; set; }
    }

}
