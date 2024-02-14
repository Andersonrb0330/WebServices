namespace WebApi.Dtos.ResponseDtos
{
    public class ProductoDto
	{
        public int Id { get; set; }

        public string Nombre { get; set; }

        public double Precio { get; set; }

        public bool Estado { get; set; }

        public int? Stock { get; set; }

        public string Descripcion { get; set; }

        public TipoProductoDto TipoProducto { get; set; }

        public ProductoDto()
		{
		}
	}
}

