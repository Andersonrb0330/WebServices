using Microsoft.AspNetCore.Mvc;
using WebApi.Dominio;
using WebApi.Dtos.RequestDtos;
using WebApi.Dtos.ResponseDtos;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/paises-old")]
    public class PaisOldController : Controller
    {
        private readonly PruebaDBContext _context;
        public PaisOldController(PruebaDBContext context) {

            _context = context; 
        }

        [HttpGet]
        public ActionResult<List<PaisDto>> ObtenerPaises()
        {
            var pais = _context.Paises.ToList();

            var paisesDto = pais.Select(p => new PaisDto
            {
                Id = p.Id,
                Nombre = p.Nombre
            }).ToList();

            return paisesDto;
        } 

        [HttpPost]
        [Route ("filtros")] 
        public ActionResult<List<PaisDto>> FiltrarPaises([FromBody] PaisFiltroParametroDto parametros)
        {
            var consulta = _context.Paises.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parametros.Nombre))
            {
                // Contains funciona como si fuera LIKE %and%erson
                consulta = consulta.Where(p => p.Nombre.Contains(parametros.Nombre));
            }

            var pais = consulta.ToList();

            var paisesDto = pais.Select(p => new PaisDto
            {
                Id = p.Id,
                Nombre = p.Nombre
            }).ToList();

            return paisesDto;
        }

        [HttpGet("{id}")]
        public ActionResult<PaisDto> ObtenerPaisPorId(int id)
        {
            // FirstOrDefault sirve para que no cause error como lo hace (First) si no encuentra dato.
            var pais = _context.Paises.FirstOrDefault(p => p.Id == id);
            if (pais == null) {
                return null;
            }

            var obtenerPais = new PaisDto
            {
                Id = pais.Id,
                Nombre = pais.Nombre,
            };

            return obtenerPais;
        }

        [HttpPost]
        public ActionResult<PaisDto> AgregarPais([FromBody] PaisParametroDto pais)
        {
            //instanciamos nuestra entiedad Pais
            var nuevoPais = new Pais
            {
                Nombre = pais.Nombre             
            };

            // add agregamos a nuevoPais
            _context.Paises.Add(nuevoPais);
            // aquì es donde recièn se guarda
            _context.SaveChanges();

            // Instanciamos nuestra entiedad Pais
            // ya guardado lo utilizamos en  PaisDto  (nuevoPais)
            var agregarPaisDto = new PaisDto
            {
                Id = nuevoPais.Id,
                Nombre = nuevoPais.Nombre
            };

            return agregarPaisDto;
        }

        [HttpPut("{id}")]
        public ActionResult<PaisDto>  ActulizarPais(int id, [FromBody] PaisParametroDto pais)
        {
            var updatePais = _context.Paises.FirstOrDefault(p => p.Id == id);
            if (updatePais == null)
            {
                return NotFound($"El Pais con id {id} no existe");
            }

            updatePais.Nombre = pais.Nombre;
         

            _context.SaveChanges();

            var updatePaisDto = new PaisDto()
            {
                Id = updatePais.Id,
                Nombre = updatePais.Nombre
            };

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

            return Ok(pais);
        }
    }
}

