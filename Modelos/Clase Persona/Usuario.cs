using System.ComponentModel.DataAnnotations;
using tpweb.Modelos.Clase_Escuela;

namespace tpweb.Modelos.Clase_Persona
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string UsuarioNombre { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;

        public int RolId { get; set; }
        public Rol Rol { get; set; } = null!;

        public List<Materia> MateriasDictadas { get; set; } = new();
    }
}
