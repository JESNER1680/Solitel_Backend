using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_Oficina")]
    public class TSOLITEL_OficinaDA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TN_IdOficina { get; set; }

        [Required]
        [StringLength(50)]
        public string TC_Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string TC_Tipo { get; set; }


    }

}
