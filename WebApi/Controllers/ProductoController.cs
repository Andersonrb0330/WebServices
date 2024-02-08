using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Dominio;
using WebApi.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/productos")]
    public class ProductoController : Controller
    {
        private readonly PruebaDBContext _context;

        public ProductoController(PruebaDBContext context)
        {
            _context = context;
        }


        [HttpGet]
        public List<Producto> ObtenerProductos()
        {
            var productos = _context.Productos.ToList();
            return productos;
        }

        /*[HttpPost]
        [Route("filtros")]
        public List<Producto> FiltrarProductos([FromBody] ProductoParametro parametros)
        {
            var productos = _context.Productos.Where(p => p.Nombre == parametros.Nombre).ToList();
            return productos;
        }*/

        [HttpPost]
        [Route("filtros")]
        public List<Producto> FiltrarProductos([FromBody] ProductoParametro parametros)
        {
            var consulta = _context.Productos.AsQueryable();
            if (!string.IsNullOrWhiteSpace(parametros.Nombre))
            {
                // Obtengo el nombre exactamente como esta escrito
                consulta = consulta.Where(p => p.Nombre == parametros.Nombre);
            }

            if (!string.IsNullOrWhiteSpace(parametros.Descripcion))
            {
                // Contains funciona como si fuera LIKE %and%erson
                consulta = consulta.Where(p => p.Descripcion.Contains(parametros.Descripcion));
            }

            if (parametros.Estado.HasValue)
            {
                // En bool se usa el true o false y con eso debemos igualar 
                consulta = consulta.Where(p => p.Estado == parametros.Estado);
            }

            if (parametros.Stock.HasValue)
            {
                consulta = consulta.Where(p => p.Stock > parametros.Stock);
            }

            var productos = consulta.ToList();
            return productos; 
        }


        [HttpGet("{id}")]
        public Producto ObtenerProductosPorId(int id)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.Id == id);
            return producto;
        }

        // POST api/values
        [HttpPost]
        public Producto AgregarProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();
            return producto;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }





        [HttpDelete("{id}")]
        public Producto DeleteProducto(int id)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.Id == id);

            _context.Productos.Remove(producto);
            _context.SaveChanges();

            return producto;
        }
    }
}

