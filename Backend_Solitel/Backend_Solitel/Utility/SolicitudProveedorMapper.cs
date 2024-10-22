using Backend_Solitel.DTO;
using BC.Modelos;
using DA.Entidades;

namespace Backend_Solitel.Utility
{
    public static class SolicitudProveedorMapper
    {
        public static SolicitudProveedor ToModel(SolicitudProveedorDTO solicitudProveedorDTO, int idProveedor)
        {
            if (solicitudProveedorDTO == null)
                return null;

            DateTime fechaActual = DateTime.Now;

            return new SolicitudProveedor
            {
                IdSolicitudProveedor = 0,
                NumeroUnico = solicitudProveedorDTO.NumeroUnico,
                NumeroCaso= solicitudProveedorDTO.NumeroCaso,
                Imputado = solicitudProveedorDTO.Imputado,
                Ofendido = solicitudProveedorDTO.Ofendido,
                Resennia = solicitudProveedorDTO.Resennia,
                DiasTranscurridos = 0,
                Urgente = solicitudProveedorDTO.Urgente,
                Aprobado = false,
                FechaCrecion = fechaActual,
                FechaModificacion = fechaActual,
                UsuarioCreador = UsuarioMapper.ToModel(solicitudProveedorDTO.UsuarioCreador),
                Delito = DelitoMapper.ToModel(solicitudProveedorDTO.Delito),
                CategoriaDelito = CategoriaDelitoMapper.ToModel(solicitudProveedorDTO.CategoriaDelito),
                Modalidad = ModalidadMapper.ToModel(solicitudProveedorDTO.Modalidad),
                Estado = EstadoMapper.ToModel(solicitudProveedorDTO.Estado),
                Proveedor = new Proveedor { TN_IdProveedor = idProveedor, TC_Nombre = "" },
                Fiscalia = FiscaliaMapper.ToModel(solicitudProveedorDTO.Fiscalia),
                Oficina = OficinaMapper.ToModel(solicitudProveedorDTO.Oficina),
                SubModalidad = SubModalidadMapper.ToModel(solicitudProveedorDTO.SubModalidad)
            };
        }

        public static SolicitudProveedorDTO ToDTO(SolicitudProveedor solicitudProveedor)
        {
            if (solicitudProveedor == null)
                return null;


            return new SolicitudProveedorDTO
            {
                IdSolicitudProveedor = solicitudProveedor.IdSolicitudProveedor,
                NumeroUnico = solicitudProveedor.NumeroUnico,
                NumeroCaso = solicitudProveedor.NumeroCaso,
                Imputado = solicitudProveedor.Imputado,
                Ofendido = solicitudProveedor.Ofendido,
                Resennia = solicitudProveedor.Resennia,
                Urgente = solicitudProveedor.Urgente,
                UsuarioCreador = UsuarioMapper.ToDTO(solicitudProveedor.UsuarioCreador),
                Delito = DelitoMapper.ToDTO(solicitudProveedor.Delito),
                CategoriaDelito = CategoriaDelitoMapper.ToDTO(solicitudProveedor.CategoriaDelito),
                Modalidad = ModalidadMapper.ToDTO(solicitudProveedor.Modalidad),
                Estado = EstadoMapper.ToDTO(solicitudProveedor.Estado),
                Fiscalia = FiscaliaMapper.ToDTO(solicitudProveedor.Fiscalia),
                Oficina = OficinaMapper.ToDTO(solicitudProveedor.Oficina),
                SubModalidad = SubModalidadMapper.ToDTO(solicitudProveedor.SubModalidad),
                Requerimientos = new List<RequerimientoProveedorDTO>(),
                Operadoras = new List<ProveedorDTO>()
            };
        }

        public static List<SolicitudProveedorDTO> ToDTO(List<SolicitudProveedor> solicitudesProveedor)
        {
            if (solicitudesProveedor == null)
                return null;

            return solicitudesProveedor.Select(c => SolicitudProveedorMapper.ToDTO(c)).ToList();
        }

    }
}
