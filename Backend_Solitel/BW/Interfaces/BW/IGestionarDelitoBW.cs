﻿using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.BW
{
    public interface IGestionarDelitoBW
    {
        public Task<Delito> insertarDelito(Delito delito);

        public Task<Delito> eliminarDelito(int id);

        public Task<List<Delito>> obtenerDelitos();
    }
}
