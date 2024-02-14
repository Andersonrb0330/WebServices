namespace WebApi.Dtos.RequestDtos
{
    public class ProductoFiltroParametroDto
	{
        public string Nombre { get; set; }
        public string Descripcion  { get; set; }
        public bool? Estado { get; set; }
        public int? Stock { get; set; }
        public int? IdTipoProducto { get; set; }

        public ProductoFiltroParametroDto()
		{
			
		}
	}
}

