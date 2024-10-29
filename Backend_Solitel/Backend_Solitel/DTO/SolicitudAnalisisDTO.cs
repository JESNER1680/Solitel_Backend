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
    }

}
