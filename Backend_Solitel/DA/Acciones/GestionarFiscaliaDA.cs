﻿using BW.Interfaces.DA;
using DA.Contexto;
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
            Entidades.FiscaliaDA nuevaFiscaliaDA = new()
            {
                Id = 0,
                Nombre = nombre,
            };

            _context.FiscaliaDA.Add(nuevaFiscaliaDA);

            var resultado = await _context.SaveChangesAsync();

            return resultado >= 0 ? true : false;
        }
    }
}
