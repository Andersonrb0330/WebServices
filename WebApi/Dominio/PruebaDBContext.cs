using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Dominio
{
    public class PruebaDBContext : DbContext
    {
        public PruebaDBContext(DbContextOptions<PruebaDBContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<TipoProducto> TipoProductos { get; set; }
        public DbSet<Usuario > Usuarios { get; set; }
        public DbSet<Pais> Paises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoProducto>()
               .HasMany(e => e.Productos)
               .WithOne(e => e.TipoProducto)
               .HasForeignKey(e => e.IdTipoProducto)
               .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<Pais>()
               .HasMany(e => e.Usuarios)
               .WithOne(e => e.Pais)
               .HasForeignKey(e => e.IdPais)
               .HasPrincipalKey(e => e.Id);
        }
    }
}

