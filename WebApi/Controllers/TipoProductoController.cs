using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dominio;
using WebApi.Dtos.RequestDtos;
using WebApi.Dtos.ResponseDtos;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/tipoProductos")]
    public class TipoProductoController : Controller
    {
        private readonly PruebaDBContext _context;
        private readonly IMapper _mapper;

        public TipoProductoController(PruebaDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult<List<TipoProductoDto>> ObtenerTipoProductos()
        {
            var tipoProducto = _context.TipoProductos.ToList();

            var tipoProductoDto = _mapper.Map<List<TipoProductoDto>>(tipoProducto);

            return tipoProductoDto;
        }

        [HttpGet("{id}")]
        public ActionResult<TipoProductoDto> ObtenerTipoProductoPorId(int id)
        {
            // FirstOrDefault sirve para que no cause error como lo hace (First) si no encuentra dato.
            var tipoProducto = _context.TipoProductos.FirstOrDefault(t => t.Id == id);

            var tipoProductoDto = _mapper.Map< TipoProductoDto>(tipoProducto);

            return tipoProductoDto;
        }

        [HttpPost]
        [Route("filtros")]
        public ActionResult<List<TipoProductoDto>> FiltrarTipoProductos([FromBody] TipoProductoFiltroParametroDto parametros)
        {
            var consulta = _context.TipoProductos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parametros.Nombre))
            {
                consulta = consulta.Where(p => p.Nombre.Contains(parametros.Nombre));
            }

            var tipoProductos = consulta.ToList();

            var tipoProductoDto = _mapper.Map<List<TipoProductoDto>>(tipoProductos);

            return tipoProductoDto;
        }

        [HttpPost]
        public ActionResult<TipoProductoDto> AgregarTipoProducto([FromBody] TipoProductoParametroDto tipoProducto)
        {
            var nuevoTipoProducto = _mapper.Map<TipoProducto>(tipoProducto);

            _context.TipoProductos.Add(nuevoTipoProducto);

            _context.SaveChanges();

            var agregarTipoProductoDto = _mapper.Map<TipoProductoDto>(nuevoTipoProducto);

            return agregarTipoProductoDto;
        }


        [HttpPut("{id}")]
        public ActionResult<TipoProductoDto> ActulizarTipoProducto(int id, [FromBody] TipoProductoParametroDto tipoProducto)
        {
            var updateTipoProducto = _context.TipoProductos.FirstOrDefault(t => t.Id == id);
            if (updateTipoProducto == null)
            {
                return NotFound($"El Tipo Producto con id {id} no existe");
            }

            updateTipoProducto.Nombre = tipoProducto.Nombre;

            _context.SaveChanges();

            var updatePaisDto = _mapper.Map<TipoProductoDto>(updateTipoProducto);

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

            return Ok();
        }
    }
}

