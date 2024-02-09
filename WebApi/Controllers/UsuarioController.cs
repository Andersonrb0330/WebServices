using Microsoft.AspNetCore.Mvc;
using WebApi.Dominio;
using WebApi.Dtos;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/usuario")]
    public class UsuarioController : Controller
    {
        private readonly PruebaDBContext _context;

        public UsuarioController(PruebaDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Usuario>> ObtenerUsuarios()
        {
            var usuarios = _context.Usuarios.ToList();
            return usuarios;
        }

        [HttpGet("{id}")]
        public ActionResult ObtenerUsuariosPorId(int id)
        {
            var usuario = _context.Usuarios .FirstOrDefault(p => p.Id == id);
            return Ok(usuario);
        }

        [HttpPost]
        [Route("filtros")]
        public ActionResult<List<Usuario>> FiltrarUsuarios([FromBody] Usuario usuario)
        {
            var consulta = _context.Usuarios.AsQueryable();
            if (!string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                // Obtengo el nombre exactamente como esta escrito
                consulta = consulta.Where(u => u.Nombre == usuario.Nombre);
            }

            if (!string.IsNullOrWhiteSpace(usuario.Apellido))
            {
                // Contains funciona como si fuera LIKE %and%erson
                consulta = consulta.Where(u => u.Apellido.Contains(usuario.Apellido));
            }

            if (usuario.Edad.HasValue && usuario.Edad <= 150)
            {
                consulta = consulta.Where(u => u.Edad == usuario.Edad);

            }else if (usuario.Edad.HasValue && usuario.Edad > 150)
            {
                return BadRequest("La edad proporcionada es inválida. La edad máxima permitida es 150 años.");
            }

            if (usuario.FechaNacimiento != DateTime.MinValue)
            {
                // Filtrar por fecha de nacimiento
                consulta = consulta.Where(u => u.FechaNacimiento == usuario.FechaNacimiento);
            }

            if (!string.IsNullOrWhiteSpace(usuario.Telefono))
            {
                // Contains funciona como si fuera LIKE %and%erson
                consulta = consulta.Where(u => u.Telefono.Contains(usuario.Telefono));
            }

            var usuarios = consulta.ToList();
            return usuarios;

        }

        [HttpPost]
        public ActionResult AgregarUsuario([FromBody] Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return Ok(usuario);
        }



        [HttpPut("{id}")]
        public ActionResult<Usuario> ActulizarUsuario(int id, [FromBody] Usuario usuario)
        {
            var updateUsuario = _context.Usuarios.FirstOrDefault(p => p.Id == id);
            if (updateUsuario == null)
            {
                return NotFound($"El Usuario con id {id} no existe");
            }

            updateUsuario.Nombre = usuario.Nombre;
            updateUsuario.Apellido = usuario.Apellido;
            updateUsuario.Edad = usuario.Edad;
            updateUsuario.FechaNacimiento = usuario.FechaNacimiento;
            updateUsuario.Telefono = usuario.Telefono;

            _context.SaveChanges();

            return Ok(updateUsuario);
        }        

        [HttpDelete("{id}")]
        public ActionResult DeleteUsuario(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(p => p.Id == id);

            if (usuario == null)
            {
                return NotFound($"El Usuario con id {id} no existe");
            }
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return Ok(usuario);
        }
    }
}
