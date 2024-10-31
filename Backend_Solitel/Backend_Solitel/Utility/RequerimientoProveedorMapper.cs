using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class RequerimientoProveedorMapper
    {
        public static RequerimientoProveedor ToModel(RequerimientoProveedorDTO requerimientoProveedorDTO)
        {
            return new RequerimientoProveedor
            {
                IdRequerimientoProveedor = 0,
                FechaInicio = requerimientoProveedorDTO.TF_FechaInicio,
                FechaFinal = requerimientoProveedorDTO.TF_FechaFinal,
                Requerimiento = requerimientoProveedorDTO.TC_Requerimiento,
                NumeroSolicitud = 0,
                tipoSolicitudes = TipoSolicitudMapper.ToModel(requerimientoProveedorDTO.tipoSolicitudes),
                datosRequeridos = DatoRequeridoMapper.ToModel(requerimientoProveedorDTO.datosRequeridos)

            };
        }

        public static RequerimientoProveedorDTO ToDTO(RequerimientoProveedor requerimientoProveedor, int idSolicitudProveedor)
        {
            return new RequerimientoProveedorDTO
            {
                TN_IdRequerimientoProveedor = requerimientoProveedor.IdRequerimientoProveedor,
                TF_FechaInicio = requerimientoProveedor.FechaInicio,
                TF_FechaFinal = requerimientoProveedor.FechaFinal,
                TC_Requerimiento = requerimientoProveedor.Requerimiento,
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
