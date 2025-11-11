namespace EmprendeLeonWeb.Models.ViewModels
{
    public class PerfilNegocioViewModel
    {
        public int IdEmprendedor { get; set; }
        public string? NombreNegocio { get; set; }
        public string? Descripcion { get; set; }
        public string? Ubicacion { get; set; }
        public string? Contacto { get; set; }
        public string? Categoria { get; set; }
        public List<ProductoViewModel> Productos { get; set; } = new();
    }

    public class ProductoViewModel
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string? Imagen { get; set; }
    }
}