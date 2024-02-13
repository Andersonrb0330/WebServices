using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dominio;
using WebApi.Dtos.RequestDtos;
using WebApi.Dtos.ResponseDtos;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
	[Route("api/paises")]
	public class PaisesController : Controller
	{
		private readonly PruebaDBContext _context;
		private readonly IMapper _mapper;

		public PaisesController(PruebaDBContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public ActionResult<List<PaisDto>> ObtenerPaises()
		{
			var pais = _context.Paises.ToList();
			var paisesDto = _mapper.Map<List<PaisDto>>(pais);

			return paisesDto;
		}

		[HttpGet("{id}")]
		public ActionResult<PaisDto> ObtenerPaisPorId(int id)
		{
			// FirstOrDefault sirve para que no cause error como lo hace (First) si no encuentra dato.
			var pais = _context.Paises.FirstOrDefault(p => p.Id == id);
			var paisDto = _mapper.Map<PaisDto>(pais);
			
			return paisDto;
		}

		[HttpPost]
		[Route("filtros")]
		public ActionResult<List<PaisDto>> FiltrarPaises([FromBody] PaisFiltroParametroDto parametros)
		{
			var consulta = _context.Paises.AsQueryable();

			if (!string.IsNullOrWhiteSpace(parametros.Nombre))
			{
				consulta = consulta.Where(p => p.Nombre.Contains(parametros.Nombre));
			}

			var pais = consulta.ToList();
			var paisesDto = _mapper.Map<List<PaisDto>>(pais);

			return paisesDto;
		}


		[HttpPost]
		public ActionResult<PaisDto> AgregarPais([FromBody] PaisParametroDto pais)
		{
			var nuevoPais = _mapper.Map<Pais>(pais);

			_context.Paises.Add(nuevoPais);

			_context.SaveChanges();

			var agregarPaisDto = _mapper.Map<PaisDto>(nuevoPais);

			return agregarPaisDto;
		}



		[HttpPut("{id}")]
		public ActionResult<PaisDto> ActulizarPais(int id, [FromBody] PaisParametroDto pais)
		{
			var updatePais = _context.Paises.FirstOrDefault(p => p.Id == id);
			if (updatePais == null)
			{
				return NotFound($"El Pais con id {id} no existe");
			}

			updatePais.Nombre = pais.Nombre;

			_context.SaveChanges();

			var updatePaisDto = _mapper.Map<PaisDto>(updatePais);

			return updatePaisDto;
		}


		[HttpDelete("{id}")]
		public ActionResult DeletePais(int id)
		{
			var pais = _context.Paises.FirstOrDefault(p => p.Id == id);

			if (pais == null)
			{
				return NotFound($"El Pais con id {id} no existe");
			}
			_context.Paises.Remove(pais);
			_context.SaveChanges();

			return Ok();
		}

	}
}

