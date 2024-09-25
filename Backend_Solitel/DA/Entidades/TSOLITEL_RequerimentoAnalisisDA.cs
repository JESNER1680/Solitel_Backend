using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_RequerimentoAnalisis")]
    public class TSOLITEL_RequerimentoAnalisisDA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TN_IdRequerimientoAnalisis { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_Objetivo { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_UtilizadoPor { get; set; }

        [Required]
        public int TN_IdTipo { get; set; }

        [Required]
        public int TN_IdAnalisis { get; set; }

        [ForeignKey("TN_IdAnalisis")]
        public virtual TSOLITEL_SolicitudAnalisisDA SolicitudAnalisis { get; set; }

        [ForeignKey("TN_IdTipo")]
        public virtual TSOLITEL_TipoDatoDA TipoDato { get; set; }
    }

}
