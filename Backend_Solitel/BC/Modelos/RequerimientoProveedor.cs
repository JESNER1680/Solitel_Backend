namespace BC.Modelos
{
    public class RequerimientoProveedor
    {
        public int IdRequerimientoProveedor { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFinal { get; set; }

        public string Requerimiento { get; set; }

        public int NumeroSolicitud { get; set; }

        public List<TipoSolicitud> tipoSolicitudes { get; set; }

        public List<DatoRequerido> datosRequeridos { get; set; }
    }

}
