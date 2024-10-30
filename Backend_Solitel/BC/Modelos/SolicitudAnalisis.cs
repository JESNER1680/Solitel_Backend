using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Modelos
{
    public class SolicitudAnalisis
    {
        public int TN_IdSolicitudAnalisis { get; set; }
        public DateTime TF_FechaDelHecho { get; set; }
        public string TC_OtrosDetalles { get; set; }
        public string? TC_OtrosObjetivosDeAnalisis { get; set; }
        public bool TB_Aprobado { get; set; }
        public DateTime? TF_FechaCrecion { get; set; }
        public int TN_NumeroSolicitud { get; set; }
        public int TN_IdOficina { get; set; }

        // Lista de requerimientos
        public List<RequerimentoAnalisis> Requerimentos { get; set; }

        // Lista de objetivos de análisis
        public List<ObjetivoAnalisis> ObjetivosAnalisis { get; set; }

        // Lista de solicitudes de proveedor
        public List<SolicitudProveedor> SolicitudesProveedor { get; set; }

        // Lista de tipos de análisis
        public List<TipoAnalisis> TiposAnalisis { get; set; }

        // Lista de condiciones
        public List<Condicion> Condiciones { get; set; }

        // Lista de archivos seleccionados para el análisis
        public List<Archivo> Archivos { get; set; }
    }

}
