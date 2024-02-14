using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Dominio;
using WebApi.Dtos.RequestDtos;
using WebApi.Dtos.ResponseDtos;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/usuario")]
    public class UsuarioController : Controller
    {
        private readonly PruebaDBContext _context;
        private readonly IMapper _mapper; 


        public UsuarioController(PruebaDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<UsuarioDto>> ObtenerUsuarios()
        {
            var usuario = _context.Usuarios.Include(u => u.Pais).ToList();
            var usuarioDto = _mapper.Map<List<UsuarioDto>>(usuario);

            return usuarioDto;
        }

        [HttpGet("{id}")]
        public ActionResult<UsuarioDto>ObtenerUsuarioPorId(int id)
        {
            // FirstOrDefault sirve para que no cause error como lo hace (First) si no encuentra dato.
            var usuario = _context.Usuarios.Include(u => u.Pais).FirstOrDefault(p => p.Id == id);
            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return usuarioDto;
        }

        [HttpPost]
        [Route("filtros")]
        public ActionResult<List<UsuarioDto>> FiltrarUsuarios([FromBody] UsuarioFiltroParametroDto parametros)
        {
            var consulta = _context.Usuarios.Include(u => u.Pais).AsQueryable();
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

            }
            else if (parametros.Edad.HasValue && parametros.Edad > 150)
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
            var usuariosDto = _mapper.Map<List<UsuarioDto>>(usuarios);

            return usuariosDto;
        }


        [HttpPost]
        public ActionResult<UsuarioDto> AgregarUsuario([FromBody] UsuarioParametroDto usuario)
        {
            var nuevoUsuario = _mapper.Map<Usuario>(usuario);

            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();

            var agregarUsuarioDto = _mapper.Map<UsuarioDto>(nuevoUsuario);
            agregarUsuarioDto.Pais = new PaisDto
            {
                Id = usuario .IdPais
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

            updateUsuario.Nombre   = usuario.Nombre;
            updateUsuario.Apellido = usuario.Apellido;
            updateUsuario.Edad     = usuario.Edad.Value;
            updateUsuario.FechaNacimiento = usuario.FechaNacimiento;
            updateUsuario.Telefono = usuario.Telefono;
            updateUsuario.IdPais   = usuario.IdPais;

            _context.SaveChanges();

            var updateUsuarioDto = _mapper.Map<UsuarioDto>(updateUsuario);

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

            return Ok();
        }
    }
}

