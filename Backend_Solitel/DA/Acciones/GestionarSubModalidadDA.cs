﻿using BC.Modelos;
using BW.Interfaces.DA;
using DA.Contexto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Acciones
{
    public class GestionarSubModalidadDA : IGestionarSubModalidadDA
    {
        private readonly SolitelContext _context;

        public GestionarSubModalidadDA(SolitelContext context)
        {
            _context = context;
        }

        public async Task<SubModalidad> insertarSubModalidad(SubModalidad subModalidad)
        {
            try
            {
                // Definir los parámetros para el procedimiento almacenado
                var nombreParam = new SqlParameter("@pTC_Nombre", subModalidad.Nombre);
                var descripcionParam = new SqlParameter("@pTC_Descripcion", subModalidad.Descripcion);
                var modalidadParam = new SqlParameter("@pTN_IdModalidad", subModalidad.IdModalidad);

                // Definir el parámetro de salida para capturar el ID generado
                var idParam = new SqlParameter("@pTN_IdSubModalidad", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarSubModalidad @pTN_IdSubModalidad OUTPUT, @pTC_Nombre, @pTC_Descripcion, @pTN_IdModalidad",
                    idParam, nombreParam, descripcionParam, modalidadParam);

                // Capturar el ID generado desde el parámetro de salida
                var nuevoId = (int)idParam.Value;
                if (nuevoId <= 0)
                {
                    throw new Exception("Error al obtener el ID de la submodalidad recién insertada.");
                }

                // Asignar el ID generado a la entidad SubModalidad
                subModalidad.IdSubModalidad = nuevoId;

                return subModalidad;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al insertar la submodalidad: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar la submodalidad: {ex.Message}", ex);
            }
        }

        public async Task<List<SubModalidad>> obtenerSubModalidad()
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar submodalidades
                var subModalidadesDA = await _context.TSOLITEL_SubModalidadDA
                    .FromSqlRaw("EXEC PA_ConsultarSubModalidad")
                    .ToListAsync();

                // Mapeo de los resultados a la entidad SubModalidad
                var subModalidades = subModalidadesDA.Select(da => new SubModalidad
                {
                    IdSubModalidad = da.TN_IdSubModalidad,
                    Nombre = da.TC_Nombre,
                    Descripcion = da.TC_Descripcion,
                    IdModalidad = da.TN_IdModalida,
                }).ToList();

                return subModalidades;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener las submodalidades: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener las submodalidades: {ex.Message}", ex);
            }
        }

        public async Task<bool> eliminarSubModalidad(int id)
        {
            try
            {
                // Definir el parámetro para el procedimiento almacenado
                var idSubModalidadParam = new SqlParameter("@pTN_IdSubModalidad", id);

                // Ejecutar el procedimiento almacenado para eliminar (lógicamente)
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_EliminarSubModalidad @pTN_IdSubModalidad", idSubModalidadParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al eliminar la submodalidad.");
                }

                // Retornar un objeto SubModalidad con el Id de la submodalidad eliminada
                return true;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al eliminar la submodalidad: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al eliminar la submodalidad: {ex.Message}", ex);
            }
        }

        public async Task<List<SubModalidad>> obtenerSubModalidadPorModalidad(int id)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar las submodalidades por modalidad
                var subModalidadesDA = await _context.TSOLITEL_SubModalidadDA
                    .FromSqlRaw("EXEC PA_ConsultarSubModalidadPorModalidad @pTN_IdModalidad = {0}", id)
                    .ToListAsync(); // Traer la lista completa

                // Verificar si hay resultados
                if (subModalidadesDA == null || !subModalidadesDA.Any())
                {
                    throw new Exception($"No se encontraron submodalidades para la modalidad con ID {id}.");
                }

                // Mapear los resultados a la entidad SubModalidad
                var subModalidades = subModalidadesDA.Select(subModalidad => new SubModalidad
                {
                    IdSubModalidad = subModalidad.TN_IdSubModalidad,
                    Nombre = subModalidad.TC_Nombre,
                    Descripcion = subModalidad.TC_Descripcion,
                    IdModalidad = subModalidad.TN_IdModalida
                }).ToList();

                return subModalidades;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error en la base de datos al obtener las submodalidades para la modalidad con ID {id}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error inesperado al obtener las submodalidades para la modalidad con ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<SubModalidad> obtenerSubModalidad(int id)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar una submodalidad por ID
                var subModalidadDA = await _context.TSOLITEL_SubModalidadDA
                    .FromSqlRaw("EXEC PA_ConsultarSubModalidad @pTN_IdSubModalidad = {0}", id)
                    .ToListAsync();  // Obtener el resultado como lista

                var subModalidad = subModalidadDA.FirstOrDefault();  // Obtener el primer elemento si hay uno

                // Verificar si se encontró la submodalidad
                if (subModalidad == null)
                {
                    throw new Exception($"No se encontró una submodalidad con el ID {id}.");
                }

                // Mapear el resultado a la entidad SubModalidad
                var subModalidadResult = new SubModalidad
                {
                    IdSubModalidad = subModalidad.TN_IdSubModalidad,
                    Nombre = subModalidad.TC_Nombre,
                    Descripcion = subModalidad.TC_Descripcion,
                    IdModalidad = subModalidad.TN_IdModalida
                };

                return subModalidadResult;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener la submodalidad con ID {id}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener la submodalidad con ID {id}: {ex.Message}", ex);
            }
        }
    }
}
