using System.ComponentModel.DataAnnotations;

namespace tpweb.Modelos.Clase_Escuela
{
    public class Escuela
    {
        [Key]
        public int IdEscuela { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public List<Curso> Cursos { get; set; } = new();
    }
}
