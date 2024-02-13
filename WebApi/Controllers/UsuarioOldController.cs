using Microsoft.AspNetCore.Mvc;
using WebApi.Dominio;
using WebApi.Dtos.RequestDtos;
using WebApi.Dtos.ResponseDtos;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/usuario-old")]
    public class UsuarioOldController : Controller
    {
        private readonly PruebaDBContext _context;

        public UsuarioOldController(PruebaDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<UsuarioDto>> ObtenerUsuarios()
        {
            var usuarios = _context.Usuarios.ToList();

            var usuariosDto = usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Edad = u.Edad,
                FechaNacimiento = u.FechaNacimiento,
                Telefono = u.Telefono,

            }).ToList();

            return usuariosDto;
        }

        [HttpGet("{id}")]
        public ActionResult<UsuarioDto>  ObtenerUsuariosPorId(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);

            if (usuario == null) {
                return null;
             } 

            var usuariosDto = new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre, 
                Apellido = usuario.Apellido, 
                Edad = usuario.Edad,
                FechaNacimiento = usuario.FechaNacimiento,
                Telefono = usuario.Telefono
            };

            return usuariosDto;
        }

        [HttpPost]
        [Route("filtros")]
        public ActionResult<List<UsuarioDto>> FiltrarUsuarios([FromBody]  UsuarioFiltroParametroDto parametros)
        {
            var consulta = _context.Usuarios.AsQueryable();
            if (!string.IsNullOrWhiteSpace(parametros.Nombre))
            {
                // Obtengo el nombre exactamente como esta escrito
                consulta = consulta.Where(u => u.Nombre == parametros.Nombre);
            }

            if (!string.IsNullOrWhiteSpace(parametros.Apellido))
            {
                // Contains funciona como si fuera LIKE %and%erson
                consulta = consulta.Where(u => u.Apellido.Contains(parametros.Apellido));
            }

            if (parametros.Edad.HasValue && parametros.Edad <= 150)
            {
                consulta = consulta.Where(u => u.Edad == parametros.Edad);

            }else if (parametros.Edad.HasValue && parametros.Edad > 150)
            {
                return BadRequest("La edad proporcionada es inválida. La edad máxima permitida es 150 años.");
            }

            if (parametros.FechaNacimiento != DateTime.MinValue)
            {
                // Filtrar por fecha de nacimiento
                consulta = consulta.Where(u => u.FechaNacimiento == parametros.FechaNacimiento);
            }

            if (!string.IsNullOrWhiteSpace(parametros.Telefono))
            {
                // Contains funciona como si fuera LIKE %and%erson
                consulta = consulta.Where(u => u.Telefono.Contains(parametros.Telefono));
            }

            var usuarios = consulta.ToList();

            var  usuariosDto = usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Edad = u.Edad,
                FechaNacimiento = u.FechaNacimiento,
                Telefono = u.Telefono,

            }).ToList();

            return usuariosDto;
        }

        [HttpPost]
        public ActionResult<UsuarioDto> AgregarUsuario([FromBody] UsuarioParametroDto usuario)
        {
            //instanciamos nuestra entiedad Usuario
            var nuevoUsuario = new Usuario
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Edad = usuario.Edad,
                FechaNacimiento = usuario.FechaNacimiento,
                Telefono = usuario.Telefono
            };

            // add agregamos a nuevoUsuario
            _context. Usuarios.Add(nuevoUsuario);
            // aquì es donde recièn se guarda
            _context.SaveChanges();         

            // Instanciamos nuestra entiedad Usuario
            // ya guardado lo utilizamos en  UsuarioDto  (nuevoUsuario)
            var agregarUsuarioDto = new  UsuarioDto            
            {
                Id = nuevoUsuario.Id,
                Nombre = nuevoUsuario.Nombre,
                Apellido = nuevoUsuario.Apellido,
                Edad = nuevoUsuario.Edad,
                FechaNacimiento = nuevoUsuario.FechaNacimiento,
                Telefono = nuevoUsuario.Telefono
            };

            return agregarUsuarioDto;
        }



        [HttpPut("{id}")]
        public ActionResult<UsuarioDto> ActulizarUsuario(int id, [FromBody] UsuarioParametroDto usuario)
        {
            var updateUsuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
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

            var updateUsuarioDto = new UsuarioDto()
            {
                Id = updateUsuario.Id,
                Nombre = updateUsuario.Nombre,
                Apellido = updateUsuario.Apellido,
                Edad = updateUsuario.Edad,
                FechaNacimiento = updateUsuario.FechaNacimiento,
                Telefono = updateUsuario.Telefono
            };

            return updateUsuarioDto;
        }        

        [HttpDelete("{id}")]
        public ActionResult DeleteUsuario(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);

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
