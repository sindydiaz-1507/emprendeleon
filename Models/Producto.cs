using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmprendeLeonWeb.Models; // ← Importa el modelo Emprendedor

namespace EmprendeLeonWeb.Models
{
    public class Producto
    {
        [Key]
        [Column("id_producto")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public string Imagen { get; set; } = string.Empty;

        [Required]
        public string Disponibilidad { get; set; } = "disponible";

        [Required]
        public decimal Precio { get; set; }

        [ForeignKey("Emprendedor")]
        [Column("id_emprendedor")]
        public int IdEmprendedor { get; set; }

        public Emprendedor Emprendedor { get; set; } = new Emprendedor();
    }
}