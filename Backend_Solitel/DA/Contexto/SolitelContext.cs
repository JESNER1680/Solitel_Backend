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
        public SolitelContext(DbContextOptions options): base(options){}

        public DbSet<TSOLITEL_FiscaliaDA> TSOLITEL_FiscaliaDA { get; set; }

        public DbSet<TSOLITEL_DelitoDA> TSOLITEL_DelitoDA { get; set; }

        public DbSet<TSOLITEL_CategoriaDelitoDA> TSOLITEL_CategoriaDelitoDA { get; set; }

        public DbSet<TSOLITEL_CondicionDA> TSOLITEL_CondicionDA { get; set; }

        public DbSet<TSOLITEL_ModalidadDA> TSOLITEL_ModalidadDA { get; set; }

        public DbSet<TSOLITEL_SubModalidadDA> TSOLITEL_SubModalidadDA { get; set; }

        public DbSet<TSOLITEL_TipoSolicitudDA> TSOLITEL_TipoSolicitudDA { get; set; }

        public DbSet<TSOLITEL_TipoDatoDA> TSOLITEL_TipoDatoDA { get; set; }
        public DbSet<TSOLITEL_SolicitudAnalisisDA> TSOLITEL_SolicitudAnalisisDA { get; set; }
        public DbSet<TSOLITEL_RequerimentoAnalisisDA > TSOLITEL_RequerimentoAnalisisDA { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TSOLITEL_FiscaliaDA>().ToTable("TSOLITEL_Fiscalia").HasKey(p => p.TN_IdFiscalia);
            modelBuilder.Entity<TSOLITEL_DelitoDA>().ToTable("TSOLITEL_Delito").HasKey(p => p.TN_IdDelito);
            modelBuilder.Entity<TSOLITEL_CategoriaDelitoDA>().ToTable("TSOLITEL_CategoriaDelitoDA").HasKey(p => p.TN_IdCategoriaDelito);
            modelBuilder.Entity<TSOLITEL_CondicionDA>().ToTable("TSOLITEL_CondicionDA").HasKey(p => p.TN_IdCondicion);
            modelBuilder.Entity<TSOLITEL_ModalidadDA>().ToTable("TSOLITEL_ModalidadDA").HasKey(p => p.TN_IdModalidad);
            modelBuilder.Entity<TSOLITEL_SubModalidadDA>().ToTable("TSOLITEL_SubModalidadDA").HasKey(p => p.TN_IdSubModalidad);
            modelBuilder.Entity<TSOLITEL_TipoSolicitudDA>().ToTable("TSOLITEL_TipoSolicitudDA").HasKey(p => p.TN_IdTipoSolicitud);
            modelBuilder.Entity<TSOLITEL_TipoDatoDA>().ToTable("TSOLITEL_TipoDatoDA").HasKey(p => p.TN_IdTipoDato);
            modelBuilder.Entity<TSOLITEL_SolicitudAnalisisDA>().ToTable("TSOLITEL_SolicitudAnalisisDA").HasKey(p => p.TN_IdSolicitudAnalisis);
            modelBuilder.Entity<TSOLITEL_RequerimentoAnalisisDA>().ToTable("TSOLITEL_RequerimentoAnalisisDA").HasKey(p => p.TN_IdRequerimientoAnalisis);
        }
    }
}
