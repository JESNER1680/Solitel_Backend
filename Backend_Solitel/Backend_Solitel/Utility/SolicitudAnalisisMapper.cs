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
                IdUsuario = solicitudAnalisisDTO.IdUsuario,

                // Mapear Requerimientos
                Requerimentos = solicitudAnalisisDTO.Requerimentos?.Select(r => new RequerimentoAnalisis
                {
                    IdRequerimientoAnalisis = r.IdRequerimientoAnalisis,
                    Objetivo = r.Objetivo,
                    UtilizadoPor = r.UtilizadoPor,
                    tipoDato = new TipoDato
                    {
                        IdTipoDato = r.tipoDatoDTO.IdTipoDato
                    },
                    IdAnalisis = r.IdAnalisis, // Asegúrate de incluir esta propiedad si es necesaria
                    condicion = new Condicion
                    {
                        IdCondicion = r.condicion.IdCondicion,
                        Nombre = r.condicion.Nombre,
                        Descripcion = r.condicion.Descripcion
                    }
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
                IdUsuario = solicitudAnalisis.IdUsuario,

                // Mapear Requerimientos
                Requerimentos = solicitudAnalisis.Requerimentos?.Select(r => new RequerimentoAnalisisDTO
                {
                    IdRequerimientoAnalisis = r.IdRequerimientoAnalisis,
                    Objetivo = r.Objetivo,
                    UtilizadoPor = r.UtilizadoPor,
                    tipoDatoDTO = new TipoDatoDTO
                    {
                        IdTipoDato = r.tipoDato.IdTipoDato,
                        Nombre = r.tipoDato.Nombre
                    },
                    condicion = new CondicionDTO
                    {
                        IdCondicion = r.condicion.IdCondicion, // Cambiado de 'c' a 'r.condicion'
                        Nombre = r.condicion.Nombre,
                        Descripcion = r.condicion.Descripcion
                    }
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
