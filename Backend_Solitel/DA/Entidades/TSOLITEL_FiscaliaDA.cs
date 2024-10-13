using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_Fiscalia")]
    public class TSOLITEL_FiscaliaDA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TN_IdFiscalia { get; set; }

        [Required]
        [StringLength(50)]
        public required string TC_Nombre { get; set; }
    }

}
