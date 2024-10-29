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

        public DbSet<TSOLITEL_ProveedorDA> TSOLITEL_ProveedorDA { get; set; }

        public DbSet<TSOLITEL_OficinaDA> TSOLITEL_OficinaDA { get; set; }

        public DbSet<TSOLITEL_SolicitudProveedorDA> TSOLITEL_SolicitudProveedorDA { get; set; }

        public DbSet<TSOLITEL_RequerimientoProveedorDA> TSOLITEL_RequerimientoProveedorDA { get; set; }

        public DbSet<TSOLITEL_DatoRequeridoDA> TSOLITEL_DatoRequeridoDA { get; set; }
        public DbSet<TSOLITEL_ObjetivoAnalisisDA> tSOLITEL_ObjetivoAnalisisDA { get; set; }

        public DbSet<TSOLITEL_TipoAnalisisDA> TSOLITEL_TipoAnalisisDA { get; set; }

        public DbSet<TSOLITEL_ArchivoDA> TSOLITEL_ArchivoDA { get; set; }

        public DbSet<TSOLITEL_HistorialDA> TSOLITEL_HistorialDA { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TSOLITEL_FiscaliaDA>().ToTable("TSOLITEL_Fiscalia").HasKey(p => p.TN_IdFiscalia);
            modelBuilder.Entity<TSOLITEL_DelitoDA>().ToTable("TSOLITEL_Delito").HasKey(p => p.TN_IdDelito);
            modelBuilder.Entity<TSOLITEL_CategoriaDelitoDA>().ToTable("TSOLITEL_CategoriaDelitoDA").HasKey(p => p.TN_IdCategoriaDelito);
            modelBuilder.Entity<TSOLITEL_CondicionDA>().ToTable("TSOLITEL_Condicion").HasKey(p => p.TN_IdCondicion);
            modelBuilder.Entity<TSOLITEL_ModalidadDA>().ToTable("TSOLITEL_Modalidad").HasKey(p => p.TN_IdModalidad);
            modelBuilder.Entity<TSOLITEL_SubModalidadDA>().ToTable("TSOLITEL_SubModalidad").HasKey(p => p.TN_IdSubModalidad);
            modelBuilder.Entity<TSOLITEL_TipoSolicitudDA>().ToTable("TSOLITEL_TipoSolicitud").HasKey(p => p.TN_IdTipoSolicitud);
            modelBuilder.Entity<TSOLITEL_TipoDatoDA>().ToTable("TSOLITEL_TipoDato").HasKey(p => p.TN_IdTipoDato);
            modelBuilder.Entity<TSOLITEL_SolicitudAnalisisDA>().ToTable("TSOLITEL_SolicitudAnalisisDA").HasKey(p => p.TN_IdSolicitudAnalisis);
            modelBuilder.Entity<TSOLITEL_RequerimentoAnalisisDA>().ToTable("TSOLITEL_RequerimentoAnalisis").HasKey(p => p.TN_IdRequerimientoAnalisis);
            modelBuilder.Entity<TSOLITEL_ProveedorDA>().ToTable("TSOLITEL_Proveedor").HasKey(p => p.TN_IdProveedor);
            modelBuilder.Entity<TSOLITEL_OficinaDA>().ToTable("TSOLITEL_Oficina").HasKey(p => p.TN_IdOficina);
            modelBuilder.Entity<TSOLITEL_SolicitudProveedorDA>().ToTable("TSOLITEL_SolicitudProveedor").HasKey(p => p.TN_IdSolicitudProveedor);
            modelBuilder.Entity<TSOLITEL_RequerimientoProveedorDA>().ToTable("TSOLITEL_RequerimientoProveedor").HasKey(p => p.TN_IdRequerimientoProveedor);
            modelBuilder.Entity<TSOLITEL_DatoRequeridoDA>().ToTable("TSOLITEL_DatoRequerido").HasKey(p => p.TN_IdDatoRequerido);
            modelBuilder.Entity<TSOLITEL_TipoAnalisisDA>().ToTable("TSOLITEL_TipoAnalisis").HasKey(p => p.TN_IdTipoAnalisis);
            modelBuilder.Entity<TSOLITEL_ObjetivoAnalisisDA>().ToTable("TSOLITEL_ObjetivoAnalisis").HasKey(p => p.TN_IdObjetivoAnalisis);
        }
    }
}
