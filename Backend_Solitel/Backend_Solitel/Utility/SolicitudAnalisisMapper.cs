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
                TN_IdSolicitudAnalisis = solicitudAnalisisDTO.TN_IdSolicitudAnalisis,
                TF_FechaDelHecho = solicitudAnalisisDTO.TF_FechaDelHecho,
                TC_OtrosDetalles = solicitudAnalisisDTO.TC_OtrosDetalles,
                TC_OtrosObjetivosDeAnalisis = solicitudAnalisisDTO.TC_OtrosObjetivosDeAnalisis,
                TB_Aprobado = solicitudAnalisisDTO.TB_Aprobado,
                TF_FechaCrecion = solicitudAnalisisDTO.TF_FechaCrecion ?? fechaActual,
                TN_NumeroSolicitud = solicitudAnalisisDTO.TN_NumeroSolicitud,
                TN_IdOficina = solicitudAnalisisDTO.TN_IdOficina,

                // Mapear Requerimientos
                Requerimentos = solicitudAnalisisDTO.Requerimentos?.Select(r => new RequerimentoAnalisis
                {
                    TN_IdRequerimientoAnalisis = r.TN_IdRequerimientoAnalisis,
                    TC_Objetivo = r.TC_Objetivo,
                    TC_UtilizadoPor = r.TC_UtilizadoPor,
                    TN_IdTipo = r.TN_IdTipo
                }).ToList() ?? new List<RequerimentoAnalisis>(),

                // Mapear Objetivos de Análisis
                ObjetivosAnalisis = solicitudAnalisisDTO.ObjetivosAnalisis?.Select(o => new ObjetivoAnalisis
                {
                    TN_IdObjetivoAnalisis = o.TN_IdObjetivoAnalisis,
                    TC_Nombre = o.TC_Nombre,
                    TC_Descripcion = o.TC_Descripcion
                }).ToList() ?? new List<ObjetivoAnalisis>(),

                // Mapear Solicitudes de Proveedor
                SolicitudesProveedor = solicitudAnalisisDTO.SolicitudesProveedor?.Select(s => new SolicitudProveedor
                {
                    IdSolicitudProveedor = s.IdSolicitudProveedor
                }).ToList() ?? new List<SolicitudProveedor>(),

                // Mapear TipoAnalisis
                TiposAnalisis = solicitudAnalisisDTO.tipoAnalisis?.Select(t => new TipoAnalisis
                {
                    TN_IdTipoAnalisis = t.IdTipoAnalisis,
                    TC_Nombre = t.Nombre
                }).ToList() ?? new List<TipoAnalisis>(),

                // Mapear Condiciones
                Condiciones = solicitudAnalisisDTO.Condiciones?.Select(c => new Condicion
                {
                    TN_IdCondicion = c.IdCondicion,
                    TC_Nombre = c.Nombre,
                    TC_Descripcion = c.Descripcion,
                }).ToList() ?? new List<Condicion>(),

                // Mapear Archivos
                Archivos = solicitudAnalisisDTO.Archivos?.Select(a => new Archivo
                {
                    TN_IdArchivo = a.TN_IdArchivo,
                    TC_Nombre = a.TC_Nombre
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
                TN_IdSolicitudAnalisis = solicitudAnalisis.TN_IdSolicitudAnalisis,
                TF_FechaDelHecho = solicitudAnalisis.TF_FechaDelHecho,
                TC_OtrosDetalles = solicitudAnalisis.TC_OtrosDetalles,
                TC_OtrosObjetivosDeAnalisis = solicitudAnalisis.TC_OtrosObjetivosDeAnalisis,
                TB_Aprobado = solicitudAnalisis.TB_Aprobado,
                TF_FechaCrecion = solicitudAnalisis.TF_FechaCrecion,
                TN_NumeroSolicitud = solicitudAnalisis.TN_NumeroSolicitud,
                TN_IdOficina = solicitudAnalisis.TN_IdOficina,

                // Mapear Requerimientos
                Requerimentos = solicitudAnalisis.Requerimentos?.Select(r => new RequerimentoAnalisisDTO
                {
                    TN_IdRequerimientoAnalisis = r.TN_IdRequerimientoAnalisis,
                    TC_Objetivo = r.TC_Objetivo,
                    TC_UtilizadoPor = r.TC_UtilizadoPor,
                    TN_IdTipo = r.TN_IdTipo
                }).ToList() ?? new List<RequerimentoAnalisisDTO>(),

                // Mapear Objetivos de Análisis
                ObjetivosAnalisis = solicitudAnalisis.ObjetivosAnalisis?.Select(o => new ObjetivoAnalisisDTO
                {
                    TN_IdObjetivoAnalisis = o.TN_IdObjetivoAnalisis,
                    TC_Nombre = o.TC_Nombre,
                    TC_Descripcion = o.TC_Descripcion
                }).ToList() ?? new List<ObjetivoAnalisisDTO>(),

                // Mapear Solicitudes de Proveedor
                SolicitudesProveedor = solicitudAnalisis.SolicitudesProveedor?.Select(s => new SolicitudProveedorDTO
                {
                    IdSolicitudProveedor = s.IdSolicitudProveedor
                }).ToList() ?? new List<SolicitudProveedorDTO>(),

                // Mapear TipoAnalisis
                tipoAnalisis = solicitudAnalisis.TiposAnalisis?.Select(t => new TipoAnalisisDTO
                {
                    IdTipoAnalisis = t.TN_IdTipoAnalisis,
                    Nombre = t.TC_Nombre
                }).ToList() ?? new List<TipoAnalisisDTO>(),

                // Mapear Condiciones
                Condiciones = solicitudAnalisis.Condiciones?.Select(c => new CondicionDTO
                {
                    IdCondicion = c.TN_IdCondicion,
                    Nombre = c.TC_Nombre,
                    Descripcion = c.TC_Descripcion
                }).ToList() ?? new List<CondicionDTO>(),

                // Mapear Archivos
                Archivos = solicitudAnalisis.Archivos?.Select(a => new ArchivoDTO
                {
                    TN_IdArchivo = a.TN_IdArchivo,
                    TC_Nombre = a.TC_Nombre
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
