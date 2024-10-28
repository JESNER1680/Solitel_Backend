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

        public DbSet<TSOLITEL_RolDA> TSOLITEL_RolDA { get; set; }

        public DbSet<TSOLITEL_PermisoDA> TSOLITEL_PermisoDA { get; set; }

        public DbSet<TSOLITEL_UsuarioDA> TSOLITEL_UsuarioDA { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TSOLITEL_OficinaDA>().ToTable("TSOLITEL_Oficina").HasKey(p => p.TN_IdOficina);

            modelBuilder.Entity<TSOLITEL_RolDA>().ToTable("TSOLITEL_Rol").HasKey(p => p.TN_IdRol);

            modelBuilder.Entity<TSOLITEL_PermisoDA>().ToTable("TSOLITEL_Permiso").HasKey(p => p.TN_IdPermiso);

            modelBuilder.Entity<TSOLITEL_UsuarioDA>().ToTable("TSOLITEL_Usuario").HasKey(p => p.TN_IdUsuario);
        }
    }
}
