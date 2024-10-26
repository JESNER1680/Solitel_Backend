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

        public DbSet<TSOLITEL_OficinaDA> TSOLITEL_OficinaDA { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TSOLITEL_OficinaDA>().ToTable("TSOLITEL_Oficina").HasKey(p => p.TN_IdOficina);
        }
    }
}
