using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_TipoDato")]
    public class TSOLITEL_TipoDatoDA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TN_IdTipoDato { get; set; }

        [Required]
        public byte[] TC_Nombre { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_Descripcion { get; set; }
    }

}
