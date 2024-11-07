using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public class SolicitudAnalisisMapper
    {
        public static SolicitudAnalisis ToModel(SolicitudAnalisisDTO solicitudAnalisisDTO)
        {
            if (solicitudAnalisisDTO == null)
                return null;

            DateTime fechaActual = DateTime.Now;

            return new SolicitudAnalisis
            {
                IdSolicitudAnalisis = solicitudAnalisisDTO.IdSolicitudAnalisis,
                FechaDelHecho = solicitudAnalisisDTO.FechaDelHecho,
                OtrosDetalles = solicitudAnalisisDTO.OtrosDetalles,
                OtrosObjetivosDeAnalisis = solicitudAnalisisDTO.OtrosObjetivosDeAnalisis,
                Aprobado = solicitudAnalisisDTO.Aprobado,
                Estado = solicitudAnalisisDTO.Estado,
                FechaCrecion = solicitudAnalisisDTO.FechaCrecion?? fechaActual,
                NumeroSolicitud = solicitudAnalisisDTO.NumeroSolicitud,
                IdOficina = solicitudAnalisisDTO.IdOficina,

                // Mapear Requerimientos
                Requerimentos = solicitudAnalisisDTO.Requerimentos?.Select(r => new RequerimentoAnalisis
                {
                    IdRequerimientoAnalisis = r.IdRequerimientoAnalisis,
                    Objetivo = r.Objetivo,
                    UtilizadoPor = r.UtilizadoPor,
                    IdTipo = r.IdTipo
                }).ToList() ?? new List<RequerimentoAnalisis>(),

                // Mapear Objetivos de Análisis
                ObjetivosAnalisis = solicitudAnalisisDTO.ObjetivosAnalisis?.Select(o => new ObjetivoAnalisis
                {
                    IdObjetivoAnalisis = o.IdObjetivoAnalisis,
                    Nombre = o.Nombre,
                    Descripcion = o.Descripcion
                }).ToList() ?? new List<ObjetivoAnalisis>(),

                // Mapear Solicitudes de Proveedor
                SolicitudesProveedor = solicitudAnalisisDTO.SolicitudesProveedor?.Select(s => new SolicitudProveedor
                {
                    IdSolicitudProveedor = s.IdSolicitudProveedor
                }).ToList() ?? new List<SolicitudProveedor>(),

                // Mapear TipoAnalisis
                TiposAnalisis = solicitudAnalisisDTO.tipoAnalisis?.Select(t => new TipoAnalisis
                {
                    IdTipoAnalisis = t.IdTipoAnalisis,
                    Nombre = t.Nombre
                }).ToList() ?? new List<TipoAnalisis>(),

                // Mapear Condiciones
                Condiciones = solicitudAnalisisDTO.Condiciones?.Select(c => new Condicion
                {
                    IdCondicion = c.IdCondicion,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion
                }).ToList() ?? new List<Condicion>(),

                // Mapear Archivos
                Archivos = solicitudAnalisisDTO.Archivos?.Select(a => new Archivo
                {
                    IdArchivo = a.IdArchivo,
                    Nombre = a.Nombre
                }).ToList() ?? new List<Archivo>()
            };
        }

        // Convierte de Modelo a DTO
        public static SolicitudAnalisisDTO ToDTO(SolicitudAnalisis solicitudAnalisis)
        {
            if (solicitudAnalisis == null)
                return null;

            return new SolicitudAnalisisDTO
            {
                IdSolicitudAnalisis = solicitudAnalisis.IdSolicitudAnalisis,
                FechaDelHecho = solicitudAnalisis.FechaDelHecho,
                OtrosDetalles = solicitudAnalisis.OtrosDetalles,
                OtrosObjetivosDeAnalisis = solicitudAnalisis.OtrosObjetivosDeAnalisis,
                Aprobado = solicitudAnalisis.Aprobado,
                Estado = solicitudAnalisis.Estado,
                FechaCrecion = solicitudAnalisis.FechaCrecion,
                NumeroSolicitud = solicitudAnalisis.NumeroSolicitud,
                IdOficina = solicitudAnalisis.IdOficina,

                // Mapear Requerimientos
                Requerimentos = solicitudAnalisis.Requerimentos?.Select(r => new RequerimentoAnalisisDTO
                {
                    IdRequerimientoAnalisis = r.IdRequerimientoAnalisis,
                    Objetivo = r.Objetivo,
                    UtilizadoPor = r.UtilizadoPor,
                    IdTipo = r.IdTipo
                }).ToList() ?? new List<RequerimentoAnalisisDTO>(),

                // Mapear Objetivos de Análisis
                ObjetivosAnalisis = solicitudAnalisis.ObjetivosAnalisis?.Select(o => new ObjetivoAnalisisDTO
                {
                    IdObjetivoAnalisis = o.IdObjetivoAnalisis,
                    Nombre = o.Nombre,
                    Descripcion = o.Descripcion
                }).ToList() ?? new List<ObjetivoAnalisisDTO>(),

                // Mapear Solicitudes de Proveedor
                SolicitudesProveedor = solicitudAnalisis.SolicitudesProveedor?.Select(s => new SolicitudProveedorDTO
                {
                    IdSolicitudProveedor = s.IdSolicitudProveedor
                }).ToList() ?? new List<SolicitudProveedorDTO>(),

                // Mapear TipoAnalisis
                tipoAnalisis = solicitudAnalisis.TiposAnalisis?.Select(t => new TipoAnalisisDTO
                {
                    IdTipoAnalisis = t.IdTipoAnalisis,
                    Nombre = t.Nombre
                }).ToList() ?? new List<TipoAnalisisDTO>(),

                // Mapear Condiciones
                Condiciones = solicitudAnalisis.Condiciones?.Select(c => new CondicionDTO
                {
                    IdCondicion = c.IdCondicion,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion
                }).ToList() ?? new List<CondicionDTO>(),

                // Mapear Archivos
                Archivos = solicitudAnalisis.Archivos?.Select(a => new ArchivoDTO
                {
                    IdArchivo = a.IdArchivo,
                    Nombre = a.Nombre
                }).ToList() ?? new List<ArchivoDTO>()
            };
        }

        // Convierte una lista de modelos a una lista de DTOs
        public static List<SolicitudAnalisisDTO> ToDTO(List<SolicitudAnalisis> solicitudesAnalisis)
        {
            if (solicitudesAnalisis == null)
                return null;

            return solicitudesAnalisis.Select(c => ToDTO(c)).ToList();
        }

        // Convierte una lista de DTOs a una lista de modelos
        public static List<SolicitudAnalisis> ToModel(List<SolicitudAnalisisDTO> solicitudesAnalisisDTO)
        {
            if (solicitudesAnalisisDTO == null)
                return null;

            return solicitudesAnalisisDTO.Select(c => ToModel(c)).ToList();
        }
    }
}
