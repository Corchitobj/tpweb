using System.ComponentModel.DataAnnotations;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Modelos.Clase_Escuela
{
    public class Tarea
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaEntrega { get; set; }

        public int MateriaId { get; set; }
        public Materia Materia { get; set; } = null!;

        public List<TareaAlumno> TareasAlumnos { get; set; } = new();
    }
}
