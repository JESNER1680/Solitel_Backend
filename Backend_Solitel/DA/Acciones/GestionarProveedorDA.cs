using BC.Modelos;
using BW.Interfaces.DA;
using DA.Contexto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DA.Acciones
{
    public class GestionarProveedorDA : IGestionarProveedorDA
    {
        private readonly SolitelContext _context;

        public GestionarProveedorDA(SolitelContext solitelContext)
        {
            this._context = solitelContext;
        }

        public async Task<Proveedor> ConsultarProveedor(int idProveedor)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar el proveedor por ID
                var proveedorData = await _context.TSOLITEL_ProveedorDA
                    .FromSqlRaw("EXEC PA_ConsultarProveedorPorId @TN_IdProveedor = {0}", idProveedor)
                    .ToListAsync();  // Convertir la consulta a una lista

                var proveedor = proveedorData.FirstOrDefault();  // Obtener el primer elemento si existe

                // Verificar si se encontró el proveedor
                if (proveedor == null)
                {
                    throw new Exception($"No se encontró un proveedor con el ID {idProveedor}.");
                }

                // Mapear los resultados a la entidad Proveedor (si es necesario)
                var proveedorEntidad = new Proveedor
                {
                    TN_IdProveedor = proveedor.TN_IdProveedor,
                    TC_Nombre = proveedor.TC_Nombre
                };

                return proveedorEntidad;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener el proveedor con ID {idProveedor}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener el proveedor con ID {idProveedor}: {ex.Message}", ex);
            }
        }

        public async Task<List<Proveedor>> ConsultarProveedores()
        {
            try
            {
                // Ejecutar el procedimiento almacenado
                var proveedoresDA = await _context.TSOLITEL_ProveedorDA
                    .FromSqlRaw("EXEC PA_ConsultarProveedor")
                    .ToListAsync();

                // Mapeo de los resultados
                var proveedores = proveedoresDA.Select(da => new Proveedor
                {
                    TN_IdProveedor = da.TN_IdProveedor,
                    TC_Nombre = da.TC_Nombre
                }).ToList();

                return proveedores;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener proveedores: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener la lista de proveedores: {ex.Message}", ex);
            }
        }

        public async Task<bool> EliminarProveedor(int idProveedor)
        {
            try
            {
                // Definir el parámetro para el procedimiento almacenado
                var idProveedorParam = new SqlParameter("@PN_IdProveedor", idProveedor);

                // Ejecutar el procedimiento almacenado para eliminar (lógicamente)
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_EliminarProveedor @PN_IdProveedor", idProveedorParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al eliminar el proveedor.");
                }

                return resultado >= 0 ? true : false;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al eliminar el proveedor: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al eliminar el proveedor: {ex.Message}", ex);
            }
        }

        public async Task<bool> InsertarProveedor(Proveedor proveedor)
        {
            try
            {
                // Definir los parámetros para el procedimiento almacenado
                var nombreParam = new SqlParameter("@PC_Nombre", proveedor.TC_Nombre);

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarProveedor @PC_Nombre",
                nombreParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al insertar el proveedor.");
                }

                return resultado >= 0 ? true : false;
            }
            catch (SqlException ex)
            {
                // Si el error proviene de SQL Server, se captura el mensaje del procedimiento almacenado
                throw new Exception($"Error en la base de datos al insertar el proveedor: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar el proveedor: {ex.Message}", ex);
            }
        }
    }
}
