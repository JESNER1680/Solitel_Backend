using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.DA
{
    public interface IGestionarRequerimientoAnalisisDA
    {
        public Task<bool> EditarRequerimientoAnalisis(RequerimentoAnalisis requerimento);
        public Task<bool> EliminarRequerimientoAnalisis(int idRequerimiento);
    }
}
