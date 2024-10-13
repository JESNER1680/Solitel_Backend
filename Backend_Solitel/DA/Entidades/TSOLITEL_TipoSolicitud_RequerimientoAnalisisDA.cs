using BC.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_TipoSolicitud_RequerimientoAnalisis")]
    public class TSOLITEL_TipoSolicitud_RequerimientoAnalisisDA
    {
        [Key, Column(Order = 0)]
        [Required]
        public int TN_IdTipoSolicitud { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        public int TN_IdRequerimientoAnalisis { get; set; }

        [ForeignKey("TN_IdTipoSolicitud")]
        public virtual TipoSolicitudDA TSOLITEL_TipoSolicitud { get; set; }

        [ForeignKey("TN_IdRequerimientoAnalisis")]
        public virtual TSOLITEL_RequerimentoAnalisisDA TSOLITEL_RequerimentoAnalisis { get; set; }
    }

}
