using DA.Entidades;
using Microsoft.EntityFrameworkCore;
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

        public DbSet<TSOLITEL_Fiscalia> TSOLITEL_Fiscalia {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TSOLITEL_Fiscalia>().ToTable("TSOLITEL_Fiscalia").HasKey(p => p.TN_IdFiscalia);
            
        }
    }
}
