using System.ComponentModel.DataAnnotations;

namespace tpweb.Modelos.Clase_Persona
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        public string Nombre { get; set; } = string.Empty; // Docente, Preceptor, Administrador

        public List<Usuario> Usuarios { get; set; } = new();
    }
}
