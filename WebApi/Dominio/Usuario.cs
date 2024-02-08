using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
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

        public DateTime FechaNacimiento { get; set; }

        public int Telefono { get; set; }


        public Usuario()
        {
        }

    }
}

