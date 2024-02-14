namespace WebApi.Dtos.ResponseDtos
{
    public class UsuarioDto
	{

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int? Edad { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Telefono { get; set; }

        public PaisDto Pais { get; set; }

        public UsuarioDto()
        {
        }

    }
}

