namespace BC.Modelos
{
    public class RequerimientoProveedor
    {
        public int TN_IdRequerimientoProveedor { get; set; }

        public DateTime TF_FechaInicio { get; set; }

        public DateTime TF_FechaFinal { get; set; }

        public string TC_Requerimiento { get; set; }

        public int TN_NumeroSolicitud { get; set; }

        public List<TipoSolicitud> tipoSolicitudes { get; set; }

        public List<DatoRequerido> datosRequeridos { get; set; }
    }

}
