using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<List<Producto>> ObtenerProductos()
        {
            var productos = _context.Productos.ToList();
            return productos;
        }

        [HttpPost]
        [Route("filtros")]
        public ActionResult<List<Producto>> FiltrarProductos([FromBody] ProductoParametro parametros)
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
        public ActionResult ObtenerProductosPorId(int id)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.Id == id);
            return Ok(producto);
        }

        // POST api/values
        [HttpPost]
        public ActionResult AgregarProducto([FromBody] Producto producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();
            return Ok(producto);
        }

        [HttpPut("{id}")]
        public ActionResult<Producto> ActulizarProducto(int id, [FromBody] Producto producto)
        {
            var updateProducto = _context.Productos.FirstOrDefault(p => p.Id == id);
            if (updateProducto == null)
            {
                return NotFound($"El Producto con id {id} no existe");
            }

            updateProducto.Nombre = producto.Nombre;
            updateProducto.Precio = producto.Precio;
            updateProducto.Estado = producto.Estado;
            updateProducto.Stock = producto.Stock;
            updateProducto.Descripcion = producto.Descripcion;

            _context.SaveChanges();

            return Ok(updateProducto);
        }



        [HttpDelete("{id}")]
        public ActionResult DeleteProducto(int id)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
            {
                return NotFound($"El Producto con id {id} no existe");
            }
            _context.Productos.Remove(producto);
            _context.SaveChanges();

            return Ok(producto);
        }
    }
}

