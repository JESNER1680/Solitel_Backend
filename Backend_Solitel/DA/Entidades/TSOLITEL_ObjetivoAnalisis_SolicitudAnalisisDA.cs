using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_ObjetivoAnalisis_SolicitudAnalisis")]
    public class TSOLITEL_ObjetivoAnalisis_SolicitudAnalisisDA
    {
        [Key, Column(Order = 0)]
        public int TN_IdObjetivo { get; set; }

        [Key, Column(Order = 1)]
        public int TN_IdAnalisis { get; set; }

        [ForeignKey("TN_IdObjetivo")]
        public virtual TSOLITEL_ObjetivoAnalisisDA ObjetivoAnalisis { get; set; }

        [ForeignKey("TN_IdAnalisis")]
        public virtual TSOLITEL_SolicitudAnalisisDA SolicitudAnalisis { get; set; }
    }

}
