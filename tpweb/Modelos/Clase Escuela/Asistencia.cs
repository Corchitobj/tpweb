using System.ComponentModel.DataAnnotations;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Modelos.Clase_Escuela
{
    public class Asistencia
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public int MateriaId { get; set; }
        public Materia Materia { get; set; } = null!;

        public List<AsistenciaAlumno> AsistenciasAlumnos { get; set; } = new();
    }
}
