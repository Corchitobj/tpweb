using System.ComponentModel.DataAnnotations;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Modelos.Clase_Escuela
{
    public class Materia
    {
        [Key]
        public int IdMateria { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public int CursoId { get; set; }
        public Curso Curso { get; set; } = null!;

        public int DocenteId { get; set; }
        public Usuario Docente { get; set; } = null!;

        public List<MateriaAlumno> MateriaAlumnos { get; set; } = new();
        public List<Asistencia> Asistencias { get; set; } = new();
        public List<Tarea> Tareas { get; set; } = new();
    }
}
