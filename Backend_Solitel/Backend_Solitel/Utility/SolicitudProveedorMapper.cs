using Backend_Solitel.DTO;
using BC.Modelos;
using DA.Entidades;

namespace Backend_Solitel.Utility
{
    public static class SolicitudProveedorMapper
    {
        // Mapeo de SolicitudProveedorDTO a SolicitudProveedor
        public static SolicitudProveedor ToModel(SolicitudProveedorDTO solicitudProveedorDTO, int idProveedor)
        {
            if (solicitudProveedorDTO == null)
                return null;

            DateTime fechaActual = DateTime.Now;

            return new SolicitudProveedor
            {
                TN_IdSolicitudProveedor = 0,
                TN_NumeroUnico = solicitudProveedorDTO.NumeroUnico,
                TN_NumeroCaso= solicitudProveedorDTO.NumeroCaso,
                TC_Imputado = solicitudProveedorDTO.Imputado,
                TC_Ofendido = solicitudProveedorDTO.Ofendido,
                TC_Resennia = solicitudProveedorDTO.Resennia,
                TN_DiasTranscurridos = 0,
                TB_Urgente = solicitudProveedorDTO.Urgente,
                TB_Aprobado = false,
                TF_FechaCrecion = fechaActual,
                TF_FechaModificacion = fechaActual,
                TN_IdUsuarioCreador = solicitudProveedorDTO.IdUsuarioCreador,
                TN_IdDelito = solicitudProveedorDTO.IdDelito,
                TN_IdCategoriaDelito = solicitudProveedorDTO.IdCategoriaDelito,
                TN_IdModalida = solicitudProveedorDTO.IdModalida,
                TN_IdEstado = solicitudProveedorDTO.IdEstado,
                TN_IdProveedor = idProveedor,
                TN_IdFiscalia = solicitudProveedorDTO.IdFiscalia,
                TN_IdOficina = solicitudProveedorDTO.IdOficina,
                TN_IdSubModalidad = solicitudProveedorDTO.IdSubModalidad
            };
        }

        public static TSOLITEL_SolicitudProveedorDA toEntity(SolicitudAnalisis solicitudAnalisis)
        {
            if (solicitudAnalisis == null)
                return null;

            return new TSOLITEL_SolicitudProveedorDA
            {

            };
        }
    }
}
