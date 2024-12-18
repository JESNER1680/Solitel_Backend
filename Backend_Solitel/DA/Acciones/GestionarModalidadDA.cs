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
    public class GestionarModalidadDA: IGestionarModalidadDA  
    {
        private readonly SolitelContext _context;

        public GestionarModalidadDA(SolitelContext context)
        {
            _context = context;
        }

        public async Task<Modalidad> insertarModalidad(Modalidad modalidad)
        {
            try
            {
                // Definir los parámetros para el procedimiento almacenado
                var nombreParam = new SqlParameter("@pTC_Nombre", modalidad.Nombre);
                var descripcionParam = new SqlParameter("@pTC_Descripcion", modalidad.Descripcion);

                // Definir el parámetro de salida para capturar el ID generado
                var idParam = new SqlParameter("@pTN_IdModalidad", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarModalidad @pTN_IdModalidad OUTPUT, @pTC_Nombre, @pTC_Descripcion",
                    idParam, nombreParam, descripcionParam
                );

                // Capturar el ID generado desde el parámetro de salida
                var nuevoId = (int)idParam.Value;
                if (nuevoId <= 0)
                {
                    throw new Exception("Error al obtener el ID de la modalidad recién insertada.");
                }

                // Asignar el ID generado a la entidad Modalidad
                modalidad.IdModalidad = nuevoId;

                return modalidad;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al insertar modalidad: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar la modalidad: {ex.Message}", ex);
            }
        }

        public async Task<List<Modalidad>> obtenerModalidad()
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar modalidades
                var modalidadesDA = await _context.TSOLITEL_ModalidadDA
                    .FromSqlRaw("EXEC PA_ConsultarModalidad")
                    .ToListAsync();

                // Mapeo de los resultados a la entidad Modalidad
                var modalidades = modalidadesDA.Select(da => new Modalidad
                {
                    IdModalidad = da.TN_IdModalidad,
                    Nombre = da.TC_Nombre,
                    Descripcion = da.TC_Descripcion,
                }).ToList();

                return modalidades;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener las modalidades: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener las modalidades: {ex.Message}", ex);
            }
        }

        public async Task<bool> eliminarModalidad(int id)
        {
            try
            {
                // Definir el parámetro para el procedimiento almacenado
                var idModalidadParam = new SqlParameter("@pTN_IdModalidad", id);

                // Ejecutar el procedimiento almacenado para eliminar (lógicamente)
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_EliminarModalidad @pTN_IdModalidad", idModalidadParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al eliminar la modalidad.");
                }

                // Retornar un objeto Modalidad con el Id de la modalidad eliminada
                return true;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al eliminar la modalidad: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al eliminar la modalidad: {ex.Message}", ex);
            }
        }

        public async Task<Modalidad> obtenerModalidad(int id)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar una modalidad por ID
                var modalidadDA = await _context.TSOLITEL_ModalidadDA
                    .FromSqlRaw("EXEC PA_ConsultarModalidad @pTN_IdModalidad = {0}", id)
                    .ToListAsync();  // Obtener el resultado como lista

                var modalidad = modalidadDA.FirstOrDefault();  // Obtener el primer elemento si hay uno

                // Verificar si se encontró la modalidad
                if (modalidad == null)
                {
                    throw new Exception($"No se encontró una modalidad con el ID {id}.");
                }

                // Mapear el resultado a la entidad Modalidad
                var modalidadResult = new Modalidad
                {
                    IdModalidad = modalidad.TN_IdModalidad,
                    Nombre = modalidad.TC_Nombre,
                    Descripcion = modalidad.TC_Descripcion
                };

                return modalidadResult;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener la modalidad con ID {id}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener la modalidad con ID {id}: {ex.Message}", ex);
            }
        }
    }
}
