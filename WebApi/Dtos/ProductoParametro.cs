namespace WebApi.Dtos
{
    public class ProductoParametro
	{
        public string Nombre { get; set; }
        public string Descripcion  { get; set; }
        public bool? Estado { get; set; }
        public int? Stock { get; set; }

        public ProductoParametro()
		{
			
		}
	}
}

