using BC.Modelos;
using BW.Interfaces.DA;
using DA.Contexto;
using DA.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Acciones
{
    public class GestionarFiscaliaDA: IGestionarFiscaliaDA
    {
        private readonly SolitelContext _context;


        public GestionarFiscaliaDA(SolitelContext context)
        {
            _context = context;
        }

        public async Task<bool> insertarFiscalia(string nombre)
        {
            var nombreParam = new SqlParameter("@pNombre", nombre);

            _context.Database.ExecuteSqlRaw("EXEC PA_InsertarFiscalia @pNombre", nombreParam);

            var resultado = await _context.SaveChangesAsync();

            return resultado >= 0 ? true : false;
        }

        public List<TSOLITEL_Fiscalia> obtenerFiscalias()
        {
            var fiscaliasDA = _context.TSOLITEL_FiscaliaDA
            .FromSqlRaw("EXEC PA_VerFiscalias")
            .ToList();

            var fiscalias = fiscaliasDA.Select(da => new TSOLITEL_Fiscalia
            {
                TN_IdFiscalia = da.TN_IdFiscalia,
                TC_Nombre = da.TC_Nombre
            }).ToList();



            return fiscalias;
        }

    }
}
