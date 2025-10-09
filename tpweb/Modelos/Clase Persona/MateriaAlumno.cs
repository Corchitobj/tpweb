using tpweb.Modelos.Clase_Escuela;

namespace tpweb.Modelos.Clase_Persona
{
    public class MateriaAlumno
    {
        public int MateriaId { get; set; }
        public Materia Materia { get; set; } = null!;

        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; } = null!;
    }
}
