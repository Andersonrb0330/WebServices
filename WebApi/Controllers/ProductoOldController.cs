using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dominio;
using WebApi.Dtos.RequestDtos;
using WebApi.Dtos.ResponseDtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/productos-old")]
    public class ProductoOldController : Controller
    {
        private readonly PruebaDBContext _context;
        private readonly IMapper _mapper;

        public ProductoOldController(PruebaDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<ProductoDto>> ObtenerProductos()
        {
            var productos = _context.Productos.ToList();
            
            var productosDto = productos.Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Estado = p.Estado,
                Stock = p.Stock,
                Descripcion = p.Descripcion,

            }).ToList();

            return productosDto;
        }

        [HttpPost]
        [Route("filtros")]
        public ActionResult<List<ProductoDto>> FiltrarProductos([FromBody] ProductoFiltroParametroDto parametros)
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

            var productosDto = productos.Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Estado = p.Estado,
                Stock = p.Stock,
                Descripcion = p.Descripcion,

            }).ToList();

            return productosDto; 
        }  


        [HttpGet("{id}")]
        public ActionResult<ProductoDto> ObtenerProductosPorId(int id)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.Id == id);
            
            var obtenerProducto = _mapper.Map<ProductoDto>(producto);
            return obtenerProducto;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<ProductoDto> AgregarProducto([FromBody] ProductoParametroDto producto)
        {
            //instanciamos nuestra entiedad Producto
            var nuevoProducto = new Producto
            {
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Estado = producto .Estado,
                Stock  = producto.Stock,
                Descripcion = producto.Descripcion
            };

            // add agregamos a nuevoProducto
            _context.Productos.Add(nuevoProducto);
            // aquì es donde recièn se guarda
            _context.SaveChanges();

            // Instanciamos nuestra entiedad Producto
            // ya guardado lo utilizamos en  productoDto  (nuevoProducto)
            var agregarProductoDto = new ProductoDto
            {
                Id     = nuevoProducto.Id,
                Nombre = nuevoProducto.Nombre,
                Precio = nuevoProducto.Precio,
                Estado = producto.Estado,
                Stock = producto.Stock,
                Descripcion = nuevoProducto.Descripcion     
            };

            return agregarProductoDto;
        }

        [HttpPut("{id}")]
        public ActionResult<ProductoDto> ActulizarProducto(int id, [FromBody] ProductoParametroDto producto)
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

            var updateProductoDto = new ProductoDto()
            {
              Id     = updateProducto.Id,
              Nombre = updateProducto.Nombre,
              Estado = updateProducto.Estado,
              Precio = updateProducto.Precio,
              Stock  = updateProducto.Stock,
              Descripcion = updateProducto.Descripcion
            };

            return updateProductoDto;
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

