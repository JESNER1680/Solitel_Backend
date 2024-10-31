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
                IdSolicitudAnalisis = solicitudAnalisisDTO.TN_IdSolicitudAnalisis,
                FechaDelHecho = solicitudAnalisisDTO.TF_FechaDelHecho,
                OtrosDetalles = solicitudAnalisisDTO.TC_OtrosDetalles,
                OtrosObjetivosDeAnalisis = solicitudAnalisisDTO.TC_OtrosObjetivosDeAnalisis,
                Aprobado = solicitudAnalisisDTO.TB_Aprobado,
                FechaCrecion = solicitudAnalisisDTO.TF_FechaCrecion ?? fechaActual,
                NumeroSolicitud = solicitudAnalisisDTO.TN_NumeroSolicitud,
                IdOficina = solicitudAnalisisDTO.TN_IdOficina,

                // Mapear Requerimientos
                Requerimentos = solicitudAnalisisDTO.Requerimentos?.Select(r => new RequerimentoAnalisis
                {
                    IdRequerimientoAnalisis = r.TN_IdRequerimientoAnalisis,
                    Objetivo = r.TC_Objetivo,
                    UtilizadoPor = r.TC_UtilizadoPor,
                    IdTipo = r.TN_IdTipo
                }).ToList() ?? new List<RequerimentoAnalisis>(),

                // Mapear Objetivos de Análisis
                ObjetivosAnalisis = solicitudAnalisisDTO.ObjetivosAnalisis?.Select(o => new ObjetivoAnalisis
                {
                    IdObjetivoAnalisis = o.TN_IdObjetivoAnalisis,
                    Nombre = o.TC_Nombre,
                    Descripcion = o.TC_Descripcion
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
                    Descripcion = c.Descripcion,
                }).ToList() ?? new List<Condicion>(),

                // Mapear Archivos
                Archivos = solicitudAnalisisDTO.Archivos?.Select(a => new Archivo
                {
                    IdArchivo = a.TN_IdArchivo,
                    Nombre = a.TC_Nombre
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
                TN_IdSolicitudAnalisis = solicitudAnalisis.IdSolicitudAnalisis,
                TF_FechaDelHecho = solicitudAnalisis.FechaDelHecho,
                TC_OtrosDetalles = solicitudAnalisis.OtrosDetalles,
                TC_OtrosObjetivosDeAnalisis = solicitudAnalisis.OtrosObjetivosDeAnalisis,
                TB_Aprobado = solicitudAnalisis.Aprobado,
                TF_FechaCrecion = solicitudAnalisis.FechaCrecion,
                TN_NumeroSolicitud = solicitudAnalisis.NumeroSolicitud,
                TN_IdOficina = solicitudAnalisis.IdOficina,

                // Mapear Requerimientos
                Requerimentos = solicitudAnalisis.Requerimentos?.Select(r => new RequerimentoAnalisisDTO
                {
                    TN_IdRequerimientoAnalisis = r.IdRequerimientoAnalisis,
                    TC_Objetivo = r.Objetivo,
                    TC_UtilizadoPor = r.UtilizadoPor,
                    TN_IdTipo = r.IdTipo
                }).ToList() ?? new List<RequerimentoAnalisisDTO>(),

                // Mapear Objetivos de Análisis
                ObjetivosAnalisis = solicitudAnalisis.ObjetivosAnalisis?.Select(o => new ObjetivoAnalisisDTO
                {
                    TN_IdObjetivoAnalisis = o.IdObjetivoAnalisis,
                    TC_Nombre = o.Nombre,
                    TC_Descripcion = o.Descripcion
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
                    TN_IdArchivo = a.IdArchivo,
                    TC_Nombre = a.Nombre
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
