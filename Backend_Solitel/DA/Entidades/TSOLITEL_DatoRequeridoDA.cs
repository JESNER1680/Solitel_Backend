using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_DatoRequerido")]
    public class TSOLITEL_DatoRequeridoDA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TN_IdDatoRequerido { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_DatoRequerido { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_Motivacion { get; set; }

        [Required]
        public int TN_IdTipoDato { get; set; }

        // Relación con TSOLITEL_TipoDato
        [ForeignKey("TN_IdTipoDato")]
        public virtual TSOLITEL_TipoDatoDA TipoDato { get; set; }
    }

}
