using BC.Modelos;

namespace Backend_Solitel.DTO
{
    public class SolicitudAnalisisDTO
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
        public List<RequerimentoAnalisisDTO> Requerimentos { get; set; }

        // Lista de objetivos de análisis
        public List<ObjetivoAnalisisDTO> ObjetivosAnalisis { get; set; }

        // Lista de solicitudes de proveedor
        public List<SolicitudProveedorDTO> SolicitudesProveedor { get; set; }
        public List<TipoAnalisisDTO> tipoAnalisis { get; set; }
        // Lista de archivos seleccionados para el análisis
        public List<ArchivoDTO> Archivos { get; set; }
    }

}
