using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_SolicitudProveedor")]
    public class TSOLITEL_SolicitudProveedorDA
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TN_IdSolicitud { get; set; }

        public string? TC_NumeroUnico { get; set; }
        public string? TC_NumeroCaso { get; set; }

        [Required]
        [StringLength(150)]
        public string TC_Imputado { get; set; }

        [Required]
        [StringLength(150)]
        public string TC_Ofendido { get; set; }

        [StringLength(255)]
        public string? TC_Resennia { get; set; }

        [Required]
        public bool TB_Urgente { get; set; }

        [Required]
        public bool TB_Aprobado { get; set; }

        [Required]
        public DateTime TF_FechaDeCreacion { get; set; }

        [Required]
        public int TN_IdUsuario { get; set; }

        [Required]
        public int TN_IdDelito { get; set; }

        public string TC_NombreDelito { get; set; }


        [Required]
        public int TN_IdCategoriaDelito { get; set; }

        public string TC_NombreCategoriaDelito { get; set; }

       
        public int? TN_IdModalidad { get; set; }

        public string? TC_NombreModalidad { get; set; }

        [Required]
        public int TN_IdEstado { get; set; }

        public string TC_NombreEstado { get; set; }

        [Required]
        public int TN_IdProveedor { get; set; }

        public string TC_NombreProveedor { get; set; }

        [Required]
        public int TN_IdFiscalia { get; set; }

        public string TC_NombreFiscalia { get; set; }

        
        public int? TN_IdSubModalidad { get; set; }

        public string? TC_NombreSubModalidad { get;set; }

        public string TC_NombreUsuario { get;set; }

    }

}
