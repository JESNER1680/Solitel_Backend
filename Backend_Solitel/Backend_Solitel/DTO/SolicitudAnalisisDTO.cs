namespace Backend_Solitel.DTO
{
    public class SolicitudAnalisisDTO
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
        public List<RequerimentoAnalisisDTO> Requerimentos { get; set; }

        // Lista de objetivos de análisis
        public List<ObjetivoAnalisisDTO> ObjetivosAnalisis { get; set; }

        // Lista de solicitudes de proveedor
        public List<SolicitudProveedorDTO> SolicitudesProveedor { get; set; }
        public List<TipoAnalisisDTO> tipoAnalisis { get; set; }

        // Lista de condiciones
        public List<CondicionDTO> Condiciones { get; set; }

        // Lista de archivos seleccionados para el análisis
        public List<ArchivoDTO> Archivos { get; set; }
    }

}
