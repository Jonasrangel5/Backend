using Fisoterapia.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace Fisoterapia.Context
{
    public class FisoterapiaDbContext : DbContext
    {
        public FisoterapiaDbContext(DbContextOptions<FisoterapiaDbContext> options) : base(options)
        {
        }

        // DbSets para las tablas
        public DbSet<TipoUsuario> TipoUsuarios { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        // Nuevas tablas añadidas
        public DbSet<Lesion> Lesiones { get; set; }         // Tabla para Lesiones
        public DbSet<InformaAvance> InformasAvance { get; set; }  // Tabla para InformaAvance
        public DbSet<Reseta> Resetas { get; set; }          // Tabla para Reseta

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para TipoUsuario y Usuario ya existentes
            modelBuilder.Entity<TipoUsuario>()
                .HasKey(t => t.IdTipoUsuario);

            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.IdUsuario);

            // Configurar las relaciones y llaves primarias para las nuevas tablas
            modelBuilder.Entity<Lesion>()
                .HasKey(l => l.IdLesion);  // Clave primaria para Lesion

            modelBuilder.Entity<InformaAvance>()
                .HasKey(i => i.IdInforma);  // Clave primaria para InformaAvance

            modelBuilder.Entity<InformaAvance>()
                .HasOne(i => i.Usuario)  // Relación con Usuario
                .WithMany()
                .HasForeignKey(i => i.IdUsuario);

            modelBuilder.Entity<InformaAvance>()
                .HasOne(i => i.Lesion)   // Relación con Lesion
                .WithMany()
                .HasForeignKey(i => i.IdLesion);

            modelBuilder.Entity<Reseta>()
                .HasKey(r => r.IdReseta);  // Clave primaria para Reseta

            modelBuilder.Entity<Reseta>()
                .HasOne(r => r.Usuario)  // Relación con Usuario
                .WithMany()
                .HasForeignKey(r => r.IdUsuario);

            modelBuilder.Entity<Reseta>()
                .HasOne(r => r.Lesion)   // Relación con Lesion
                .WithMany()
                .HasForeignKey(r => r.IdLesion);
        }
    }
}
