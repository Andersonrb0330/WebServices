using Microsoft.EntityFrameworkCore;

namespace WebApi.Dominio
{
    public class PruebaDBContext : DbContext
    {
        public PruebaDBContext(DbContextOptions<PruebaDBContext> options) : base(options)
        {

        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<TipoProducto> tipoProductos { get; set; }
    }
}

