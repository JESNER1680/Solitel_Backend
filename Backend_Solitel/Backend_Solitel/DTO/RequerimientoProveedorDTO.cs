namespace Backend_Solitel.DTO
{
    public class RequerimientoProveedorDTO
    {
        public int IdRequerimientoProveedor { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFinal { get; set; }

        public string Requerimiento { get; set; }

        public List<TipoSolicitudDTO> tipoSolicitudes { get; set; }

        public List<DatoRequeridoDTO> datosRequeridos { get; set; }

    }

}
