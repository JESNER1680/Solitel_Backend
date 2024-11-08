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
                Proveedor = new Proveedor { IdProveedor = idProveedor, Nombre = "" },
                Fiscalia = FiscaliaMapper.ToModel(solicitudProveedorDTO.Fiscalia),
                Oficina = OficinaMapper.ToModel(solicitudProveedorDTO.Oficina),
                SubModalidad = SubModalidadMapper.ToModel(solicitudProveedorDTO.SubModalidad)
            };
        }

        public static SolicitudProveedorDTO ToDTO(SolicitudProveedor solicitudProveedor)
        {
            if (solicitudProveedor == null)
                return null;


            var solicitudProveedorDTO = new SolicitudProveedorDTO
            {
                IdSolicitudProveedor = solicitudProveedor.IdSolicitudProveedor,
                NumeroUnico = solicitudProveedor.NumeroUnico,
                NumeroCaso = solicitudProveedor.NumeroCaso,
                Imputado = solicitudProveedor.Imputado,
                Ofendido = solicitudProveedor.Ofendido,
                Resennia = solicitudProveedor.Resennia,
                Urgente = solicitudProveedor.Urgente,
                FechaCreacion = solicitudProveedor.FechaCrecion,
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


            solicitudProveedorDTO.Operadoras.Add(ProveedorMapper.ToDTO(solicitudProveedor.Proveedor));

            return solicitudProveedorDTO;
        }

        public static List<SolicitudProveedorDTO> ToDTO(List<SolicitudProveedor> solicitudesProveedor)
        {
            if (solicitudesProveedor == null)
                return null;

            return solicitudesProveedor.Select(c => SolicitudProveedorMapper.ToDTO(c)).ToList();
        }

        public static List<SolicitudFiltradaProveedorDTO> FiltrarListaSolicitudesProveedor(List<SolicitudProveedor> solicitudesProveedor)
        {
            List<SolicitudFiltradaProveedorDTO> listaFiltrada = new List<SolicitudFiltradaProveedorDTO>();
            foreach (SolicitudProveedor solicitudProveedor in solicitudesProveedor)
            {
                listaFiltrada.Add(new SolicitudFiltradaProveedorDTO
                {
                    IdSolicitudProveedor = solicitudProveedor.IdSolicitudProveedor,
                    NombreProveedor = solicitudProveedor.Proveedor.Nombre,
                    NumeroUnico = solicitudProveedor.NumeroUnico
                });
            }
            return listaFiltrada;
        }

        public static SolicitudProveedorInfoComunDTO FiltrarInformacionEnComun(SolicitudProveedor solicitudProveedor)
        {
            return new SolicitudProveedorInfoComunDTO
            {
                FiscaliaDTO = FiscaliaMapper.ToDTO(solicitudProveedor.Fiscalia),
                DelitoDTO = DelitoMapper.ToDTO(solicitudProveedor.Delito),
                CategoriaDelitoDTO = CategoriaDelitoMapper.ToDTO(solicitudProveedor.CategoriaDelito),
                imputado = solicitudProveedor.Imputado,
                ofendido = solicitudProveedor.Ofendido,
                resennia = solicitudProveedor.Resennia

            };
        }

    }
}
