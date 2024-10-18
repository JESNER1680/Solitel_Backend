using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class RequerimientoProveedorMapper
    {
        public static RequerimientoProveedor ToModel(RequerimientoProveedorDTO requerimientoProveedorDTO, int idSolicitudProveedor)
        {
            return new RequerimientoProveedor
            {
                TN_IdRequerimientoProveedor = 0,
                TF_FechaInicio = requerimientoProveedorDTO.TF_FechaInicio,
                TF_FechaFinal = requerimientoProveedorDTO.TF_FechaFinal,
                TC_Requerimiento = requerimientoProveedorDTO.TC_Requerimiento,
                TN_NumeroSolicitud = idSolicitudProveedor,
                tipoSolicitudes = TipoSolicitudMapper.ToModel(requerimientoProveedorDTO.tipoSolicitudes),
                datosRequeridos = DatoRequeridoMapper.ToModel(requerimientoProveedorDTO.datosRequeridos)

            };
        }
    }
}
