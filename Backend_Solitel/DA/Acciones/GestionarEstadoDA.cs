﻿using BC.Modelos;
using BW.Interfaces.DA;
using DA.Contexto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Acciones
{
    public class GestionarEstadoDA : IGestionarEstadoDA
    {
        private readonly SolitelContext _context;

        public GestionarEstadoDA(SolitelContext solitelContext)
        {
            _context = solitelContext;
        }

        public async Task<List<Estado>> ObtenerEstados()
        {
            try
            {
                // Ejecutar el procedimiento almacenado
                var estadosDA = await _context.TSOLITEL_EstadoDA
                    .FromSqlRaw("EXEC PA_ConsultarEstados")
                    .ToListAsync();

                // Mapeo de los resultados
                var estados = estadosDA.Select(da => new Estado
                {
                    TN_IdEstado = da.TN_IdEstado,
                    TC_Nombre = da.TC_Nombre,
                    TC_Descripcion = da.TC_Descripcion,
                    TC_Tipo = da.TC_Tipo

                }).ToList();

                return estados;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener estados: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener la lista de estados: {ex.Message}", ex);
            }
        }
    }
}