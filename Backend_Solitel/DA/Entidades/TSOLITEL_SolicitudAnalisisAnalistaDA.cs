using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [NotMapped]
    public class TSOLITEL_SolicitudAnalisisAnalistaDA
    {
        [Key]
        public int TN_IdAnalisis { get; set; }

        public DateTime TF_FechaDeHecho { get; set; }

        [StringLength(255)]
        public string TC_OtrosDetalles { get; set; }

        [StringLength(255)]
        public string? TC_OtrosObjetivosDeAnalisis { get; set; } // Nullable

        public DateTime? TF_FechaDeCreacion { get; set; } // Nullable

        public bool TB_Aprobado { get; set; }

        public int TN_IdEstado { get; set; }

        [StringLength(100)]
        public string TC_NombreEstado { get; set; }

        public int TN_IdOficinaCreacion { get; set; }

        [StringLength(200)]
        public string TC_NombreUsuarioCreador { get; set; }

        [StringLength(100)]
        public string TC_NombreOficina { get; set; }

        [StringLength(200)]
        public string? TC_NombreUsuarioAprobador { get; set; } // Nullable

        public DateTime? TF_FechaAprobacion { get; set; } // Nullable

        public DateTime? TF_FechaAnalizado { get; set; } // Nullable

        [StringLength(200)]
        public string? TC_NombreUsuarioAsignado { get; set; } // Nullable

        public DateTime? TF_FechaAsignacion { get; set; } // Nullable

    }
}
