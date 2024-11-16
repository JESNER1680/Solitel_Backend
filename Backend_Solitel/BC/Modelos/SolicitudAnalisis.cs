using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Modelos
{
    public class SolicitudAnalisis
    {
        public int IdSolicitudAnalisis { get; set; }

        public DateTime FechaDelHecho { get; set; }

        public string OtrosDetalles { get; set; }

        public string? OtrosObjetivosDeAnalisis { get; set; } // Nullable

        public bool Aprobado { get; set; }

        public Estado Estado { get; set; }

        public DateTime? FechaCreacion { get; set; } // Nullable

        public int IdOficinaSolicitante { get; set; }

        public int IdOficinaCreacion { get; set; }

        public int IdUsuarioCreador { get; set; }

        public int? IdUsuarioAprobador { get; set; } // Nullable (assumes this can be null based on similar cases)

        public DateTime? FechaDeAprobacion { get; set; } // Nullable

        public DateTime? FechaDeAnalizado { get; set; } // Nullable

        public string NombreUsuarioCreador { get; set; }
        public Usuario usuarioCreador { get;set; }

        public string NombreOficina { get; set; }

        public string? NombreUsuarioAprobador { get; set; } // Nullable

        public string? NombreUsuarioAsignado { get; set; } // Nullable

        public DateTime? FechaDeAsignacion { get; set; } // Nullable

        // Lista de requerimientos
        public List<RequerimentoAnalisis> Requerimentos { get; set; }

        // Lista de objetivos de análisis
        public List<ObjetivoAnalisis> ObjetivosAnalisis { get; set; }

        // Lista de solicitudes de proveedor
        public List<SolicitudProveedor> SolicitudesProveedor { get; set; }

        // Lista de tipos de análisis
        public List<TipoAnalisis> TiposAnalisis { get; set; }

        // Lista de archivos seleccionados para el análisis
        public List<Archivo> Archivos { get; set; }
    }

}
