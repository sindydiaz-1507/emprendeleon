using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmprendeLeonWeb.Models
{
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public string Contrasena { get; set; } = string.Empty;

        [Column("tipo_usuario")]
        [Required]
        public string TipoUsuario { get; set; } = "emprendedor";

        public ICollection<Emprendedor> Emprendedores { get; set; } = new List<Emprendedor>();
    }
}