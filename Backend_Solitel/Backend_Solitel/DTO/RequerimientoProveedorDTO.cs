namespace Backend_Solitel.DTO
{
    public class RequerimientoProveedorDTO
    {
        public int TN_IdRequerimientoProveedor { get; set; }

        public DateTime TF_FechaInicio { get; set; }

        public DateTime TF_FechaFinal { get; set; }

        public string TC_Requerimiento { get; set; }

        public List<TipoSolicitudDTO> tipoSolicitudes { get; set; }

        public List<DatoRequeridoDTO> datosRequeridos { get; set; }

        //public int TN_NumeroSolicitud { get; set; }
    }

}
