using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    public class TSOLITEL_ArchivoDA
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TN_IdArchivo { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_HashArchivo { get; set; }

        [Required]
        public string TC_Nombre { get; set; }

        [Required]
        public byte[] TV_Contenido { get; set; }

        [Required]
        [StringLength(50)]
        public string TC_FormatoArchivo { get; set; }

        [Required]
        public DateTime TF_FechaModificacion { get; set; }

    }
}
