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
                FechaInicio = requerimientoProveedorDTO.FechaInicio,
                FechaFinal = requerimientoProveedorDTO.FechaFinal,
                Requerimiento = requerimientoProveedorDTO.Requerimiento,
                NumeroSolicitud = 0,
                tipoSolicitudes = TipoSolicitudMapper.ToModel(requerimientoProveedorDTO.tipoSolicitudes),
                datosRequeridos = DatoRequeridoMapper.ToModel(requerimientoProveedorDTO.datosRequeridos)

            };
        }

        public static RequerimientoProveedorDTO ToDTO(RequerimientoProveedor requerimientoProveedor, int idSolicitudProveedor)
        {
            return new RequerimientoProveedorDTO
            {
                IdRequerimientoProveedor = requerimientoProveedor.IdRequerimientoProveedor,
                FechaInicio = requerimientoProveedor.FechaInicio,
                FechaFinal = requerimientoProveedor.FechaFinal,
                Requerimiento = requerimientoProveedor.Requerimiento,
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
