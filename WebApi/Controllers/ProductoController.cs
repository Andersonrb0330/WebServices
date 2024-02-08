using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Dominio;

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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

