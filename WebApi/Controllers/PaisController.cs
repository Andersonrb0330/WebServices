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
        public List<Pais> ObtenerPaises()
        {
            var paises = _context.Paises.ToList();
            return paises;
        }

        [HttpGet("{id}")]
        public Pais ObtenerPaisPorId(int id)
        {
           // var pais = _context.Paises.Where(p => p.Id == id).FirstOrDefault();
            var pais = _context.Paises.FirstOrDefault(p => p.Id == id);

            return pais;
        }

        [HttpPost]
        public Pais AgregarPais(Pais pais)
        {
            _context.Paises.Add(pais);
            _context.SaveChanges();
            return pais;
        }



    }
}

