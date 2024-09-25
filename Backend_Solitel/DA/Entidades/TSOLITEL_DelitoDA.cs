using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_Delito")]
    public class TSOLITEL_DelitoDA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TN_IdDelito { get; set; }

        [Required]
        [StringLength(50)]
        public string TC_Nombre { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_Descripcion { get; set; }

        [Required]
        public int TN_IdCategoriaDelito { get; set; }

        // Relación con TSOLITEL_CategoriaDelito
        [ForeignKey("TN_IdCategoriaDelito")]
        public virtual TSOLITEL_CategoriaDelitoDA CategoriaDelito { get; set; }
    }

}
