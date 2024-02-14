using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Dominio;
using WebApi.Dtos.RequestDtos;
using WebApi.Dtos.ResponseDtos;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/productos")]
    public class ProductoController : Controller
    {

        private readonly PruebaDBContext _context;
        private readonly IMapper _mapper;

        public ProductoController(PruebaDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<ProductoDto>> ObtenerProductos()
        {
            var producto = _context.Productos.Include( p => p.TipoProducto).ToList();
            var productoDto = _mapper.Map<List<ProductoDto>>(producto);

            return productoDto;
        }


        [HttpGet("{id}")]
        public ActionResult<ProductoDto> ObtenerProductoPorId(int id)
        {
            // FirstOrDefault sirve para que no cause error como lo hace (First) si no encuentra dato.
            var producto = _context.Productos.Include(p => p.TipoProducto)
                .FirstOrDefault(p => p.Id == id);
            var productoDto = _mapper.Map<ProductoDto>(producto);

            return productoDto;
        }

        [HttpPost]
        [Route("filtros")]
        public ActionResult<List<ProductoDto>> FiltrarProductos([FromBody] ProductoFiltroParametroDto parametros)
        {
            var consulta = _context.Productos.Include(p => p.TipoProducto).AsQueryable();
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

            if (parametros.IdTipoProducto.HasValue)
            {
                consulta = consulta.Where(p => p.IdTipoProducto == parametros.IdTipoProducto);
            }

            var productos = consulta.ToList();

            var productosDto = _mapper.Map<List<ProductoDto>>(productos);

            return productosDto;
        }


        [HttpPost]
        public ActionResult<ProductoDto> AgregarProducto([FromBody] ProductoParametroDto producto)
        {
            var nuevoProducto = _mapper.Map<Producto>(producto);

            _context.Productos.Add(nuevoProducto);
            _context.SaveChanges();

            var agregarProductoDto = _mapper.Map<ProductoDto>(nuevoProducto);
            agregarProductoDto.TipoProducto = new TipoProductoDto
            {
                Id = producto.IdTipoProducto
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

            updateProducto.Nombre   = producto.Nombre;
            updateProducto.Precio   = producto.Precio;
            updateProducto.Estado   = producto.Estado;
            updateProducto.Stock    = producto.Stock;
            updateProducto.Descripcion = producto.Descripcion;
            updateProducto.IdTipoProducto = producto.IdTipoProducto;

            _context.SaveChanges();

            var updateProductoDto = _mapper.Map<ProductoDto>(updateProducto);

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

