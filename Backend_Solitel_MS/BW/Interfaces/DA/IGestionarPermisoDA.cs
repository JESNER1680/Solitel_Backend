using DA.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.DA
{
    public interface IGestionarPermisoDA
    {
        public Task<Permiso> InsertarPermiso (Permiso permiso);

        public Task<List<Permiso>> ObtenerPermisos();

        public Task<Permiso> ObtenerPersmiso(int id);

        public Task<bool> EliminarPermiso(int id);
    }
}
