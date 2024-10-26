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

        public static RequerimientoProveedorDTO ToDTO(RequerimientoProveedor requerimientoProveedor, int idSolicitudProveedor)
        {
            return new RequerimientoProveedorDTO
            {
                TN_IdRequerimientoProveedor = requerimientoProveedor.TN_IdRequerimientoProveedor,
                TF_FechaInicio = requerimientoProveedor.TF_FechaInicio,
                TF_FechaFinal = requerimientoProveedor.TF_FechaFinal,
                TC_Requerimiento = requerimientoProveedor.TC_Requerimiento,
                tipoSolicitudes = TipoSolicitudMapper.ToDTO(requerimientoProveedor.tipoSolicitudes),
                datosRequeridos = DatoRequeridoMapper.ToDTO(requerimientoProveedor.datosRequeridos)

            };
        }

        public static List<RequerimientoProveedorDTO> ToDTO(List<RequerimientoProveedor> requerimientosProveedor, int idSolicitudProveedor)
        {
            if (requerimientosProveedor == null)
                return null;

            return requerimientosProveedor.Select(c => RequerimientoProveedorMapper.ToDTO(c, idSolicitudProveedor)).ToList();
        }
    }
}
