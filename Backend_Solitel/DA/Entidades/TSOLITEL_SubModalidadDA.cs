using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_SubModalidad")]
    public class TSOLITEL_SubModalidadDA
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TN_IdSubModalidad { get; set; }

        [Required]
        [StringLength(50)]
        public string TC_Nombre { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_Descripcion { get; set; }

        [Required]
        public int TN_IdModalida { get; set; }

        [ForeignKey("TN_IdModalida")]
        public virtual TSOLITEL_ModalidadDA Modalidad { get; set; }
    }

}
