using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class ProveedorMapper
    {
        public static Proveedor ToModel(ProveedorDTO proveedorDTO)
        {
            return new Proveedor
            {
                TN_IdProveedor = proveedorDTO.TN_IdProveedor,
                TC_Nombre = proveedorDTO.TC_Nombre
            };
        }

        public static ProveedorDTO ToDTO(this Proveedor proveedor)
        {
            if (proveedor == null)
                return null;

            return new ProveedorDTO
            {
                TN_IdProveedor = proveedor.TN_IdProveedor,
                TC_Nombre = proveedor.TC_Nombre
            };
        }

        public static List<ProveedorDTO> ToDTO(this List<Proveedor> proveedores)
        {
            if (proveedores == null)
                return null;

            return proveedores.Select(c => c.ToDTO()).ToList();
        }
    }

    
}
