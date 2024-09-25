using DA.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Contexto
{
    public class SolitelContext : DbContext
    {
        public SolitelContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<FiscaliaDA> FiscaliaDA {  get; set; } 
    }
}
