using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace WebApi.Dominio
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Apellido { get; set; }

        public int Edad { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaNacimiento { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(20)]
        public string Telefono { get; set; }


        public Usuario()
        {
        }

    }
}

