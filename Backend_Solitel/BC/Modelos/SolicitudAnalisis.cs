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
        public string? OtrosObjetivosDeAnalisis { get; set; }
        public bool Aprobado { get; set; }
        public Estado Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int IdOficinaSolicitante { get; set; }
        public int IdOficinaCreacion { get; set; }
        public int IdUsuarioCreador { get; set; }
        public int IdUsuarioAprobador { get; set; }
        public DateTime FechaDeAprobacion { get; set; }
        public DateTime FechaDeAnalizado { get; set; }

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
