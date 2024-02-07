using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApi.Dominio
{
    public class Producto
	{
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Nombre { get; set; }

        public double Precio { get; set; }

        public bool Estado { get; set; }

        public int? Stock { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(250)]
        public string Descripcion { get;  set; }

        public Producto()
		{

		}
	}
}

