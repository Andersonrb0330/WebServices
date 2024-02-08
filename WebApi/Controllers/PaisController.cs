using Microsoft.AspNetCore.Mvc;
using WebApi.Dominio;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/paises")]
    public class PaisController : Controller
    {
        private readonly PruebaDBContext _context;
        public PaisController(PruebaDBContext context) {

            _context = context; 
        }

        [HttpGet]
        public ActionResult<List<Pais>> ObtenerPaises()
        {
            var paises = _context.Paises.ToList();

            return Ok(paises);
        } 

        [HttpGet]
        [Route ("filtros")] 
        public ActionResult<List<Pais>> FiltrarPaises(string nombre)
        {       
          //  var paises = _context.Paises.Where(p => p.Nombre == nombre);
          //rusia.contiene(ia)
            var paises = _context.Paises.Where(p => p.Nombre.Contains(nombre));

            return Ok(paises);
        }

        [HttpGet("{id}")]
        public ActionResult<Pais> ObtenerPaisPorId(int id)
        {
            // FirstOrDefault sirve para que no cause error como lo hace (First) si no encuentra dato.
            var pais = _context.Paises.FirstOrDefault(p => p.Id == id);

            return Ok(pais);
        }

        [HttpPost]
        public ActionResult<Pais> AgregarPais([FromBody] Pais pais)
        {
            _context.Paises.Add(pais);
            _context.SaveChanges();
            return Ok(pais);
        }

        [HttpPut("{id}")]
        public ActionResult<Pais>  ActulizarPais(int id, [FromBody] Pais pais)
        {
            var  updatePais = _context.Paises.FirstOrDefault(p => p.Id == id);
            if (updatePais == null) {
                return NotFound($"El Paìs con id {id} no existe");
            }

            updatePais.Nombre =  pais.Nombre;

            _context.SaveChanges();

            return Ok(updatePais);
        }


        [HttpDelete("{id}")]
        public ActionResult DeletePais(int id)
        {
            var pais = _context.Paises.FirstOrDefault(p => p.Id == id);
            if (pais == null)
            {
                return NotFound($"El Paìs con id {id} no existe");
            }

            _context.Paises.Remove(pais);
            _context.SaveChanges();

            return  Ok();
        }
    }
}

