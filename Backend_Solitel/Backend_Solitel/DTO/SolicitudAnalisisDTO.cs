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
        public DateTime? FechaCrecion { get; set; }
        public int NumeroSolicitud { get; set; }
        public int IdOficina { get; set; }
        public int IdUsuario { get; set; }

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
