using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmprendeLeonWeb.Models
{
    public class Emprendedor
    {
        [Key]
        [Column("id_emprendedor")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmprendedor { get; set; }

        [Required]
        [Column("nombre_negocio")]
        public string? NombreNegocio { get; set; }

        public string Categoria { get; set; } = "General";
        public bool Visible { get; set; } = true;
        public string? Descripcion { get; set; }

        [Column("tipo_perfil")]
        [Required]
        public string TipoPerfil { get; set; } = "Gratis";


        public string? Ubicacion { get; set; }
        public string? Contacto { get; set; }

        [ForeignKey("Usuario")]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }

        public ICollection<Producto>? Productos { get; set; }
    }
}