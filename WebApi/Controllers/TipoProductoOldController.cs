using Microsoft.AspNetCore.Mvc;
using WebApi.Dominio;
using WebApi.Dtos.RequestDtos;
using WebApi.Dtos.ResponseDtos;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/tipoProductos-old")]
    public class TipoProductoOldController : Controller
    {
        private readonly PruebaDBContext _context;
        public TipoProductoOldController(PruebaDBContext context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult<List<TipoProductoDto>> ObtenerTipoProductos()
        {
            var tipoProducto = _context.TipoProductos.ToList();

            var tipoProductoDto = tipoProducto.Select(p => new TipoProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre
            }).ToList();

            return tipoProductoDto;
        }

        [HttpGet("{id}")]
        public ActionResult<TipoProductoDto> ObtenerTipoProductoPorId(int id)
        {
            // FirstOrDefault sirve para que no cause error como lo hace (First) si no encuentra dato.
            var tipoProducto = _context.TipoProductos.FirstOrDefault(t => t.Id == id);
            if (tipoProducto == null)
            {
                return null;
            } 

            var obtenerTipoProducto = new TipoProductoDto
            {
                Id = tipoProducto.Id,
                Nombre = tipoProducto.Nombre,
            };

            return obtenerTipoProducto;
        }


        [HttpPost]
        [Route("filtros")]
        public ActionResult<List<TipoProductoDto>> FiltrarTipoProductos([FromBody] TipoProductoFiltroParametroDto parametros)
        {
            var consulta = _context.TipoProductos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parametros.Nombre))
            {
                // Contains funciona como si fuera LIKE %and%erson
                consulta = consulta.Where(p => p.Nombre.Contains(parametros.Nombre));
            }

            var tipoProducto = consulta.ToList();

            var tipoProductoDto = tipoProducto.Select(t => new TipoProductoDto
            {
                Id = t.Id,
                Nombre = t.Nombre
            }).ToList();

            return tipoProductoDto;
        }


        [HttpPost]
        public ActionResult<TipoProductoDto> AgregarPais([FromBody] TipoProductoParametroDto tipoProducto)
        {
            //instanciamos nuestra entiedad TipoProducto
            var nuevoTipoProducto = new TipoProducto
            {
                Nombre = tipoProducto.Nombre
            };

            // add agregamos a  nuevoTipoProducto
            _context.TipoProductos.Add(nuevoTipoProducto);
            // aquì es donde recièn se guarda
            _context.SaveChanges();

            // Instanciamos nuestra entiedad TipoProducto
            // ya guardado lo utilizamos en  TipoProductoDto  (nuevoTipoProducto)
            var agregarTipoProductoDto = new TipoProductoDto
            {
                Id = nuevoTipoProducto.Id,
                Nombre = nuevoTipoProducto.Nombre
            };

            return agregarTipoProductoDto;
        }

        [HttpPut("{id}")]
        public ActionResult<TipoProductoDto> ActulizarPais(int id, [FromBody] TipoProductoParametroDto tipoProducto)
        {
            var updateTipoProducto = _context.TipoProductos.FirstOrDefault(t => t.Id == id);
            if (updateTipoProducto == null)
            {
                return NotFound($"El Tipo Producto con id {id} no existe");
            }

            updateTipoProducto.Nombre = tipoProducto.Nombre;


            _context.SaveChanges();

            var updatePaisDto = new TipoProductoDto()
            {
                Id = updateTipoProducto.Id,
                Nombre = updateTipoProducto.Nombre
            };

            return updatePaisDto;
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePais(int id)
        {
            var tipoProducto = _context.TipoProductos.FirstOrDefault(t => t.Id == id);

            if (tipoProducto == null)
            {
                return NotFound($"El Tipo Producto con id {id} no existe");
            }
            _context.TipoProductos.Remove(tipoProducto);
            _context.SaveChanges();

            return Ok(tipoProducto);
        }


    }
}

