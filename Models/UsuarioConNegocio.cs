namespace EmprendeLeonWeb.Models
{
    public class UsuarioConNegocio
    {
        public Usuario Usuario { get; set; } = new Usuario();
        public Emprendedor? Negocio { get; set; }
    }
}