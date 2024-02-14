using System;
namespace WebApi.Dtos.RequestDtos
{
	public class ProductoParametroDto
	{
        public string Nombre { get; set; }

        public double Precio { get; set; }

        public bool Estado { get; set; }

        public int? Stock { get; set; }

        public string Descripcion { get; set; }

        public int IdTipoProducto { get; set; }


         public ProductoParametroDto()
		{
		}
	}
}

