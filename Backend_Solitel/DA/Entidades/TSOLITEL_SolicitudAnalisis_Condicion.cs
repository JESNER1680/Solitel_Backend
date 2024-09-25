using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_SolicitudAnalisis_Condicion")]
    public class TSOLITEL_SolicitudAnalisis_CondicionDA
    {
        [Key]
        [Column(Order = 1)]
        [Required]
        public int TN_IdAnalisis { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required]
        public int TN_IdCondicion { get; set; }

        [ForeignKey("TN_IdAnalisis")]
        public virtual TSOLITEL_SolicitudAnalisisDA SolicitudAnalisis { get; set; }

        [ForeignKey("TN_IdCondicion")]
        public virtual TSOLITEL_CondicionDA Condicion { get; set; }
    }


}
