using tpweb.Modelos.Clase_Escuela;

namespace tpweb.Modelos.Clase_Persona
{
    public class AsistenciaAlumno
    {
        public int AsistenciaId { get; set; }
        public Asistencia Asistencia { get; set; } = null!;

        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; } = null!;

        public bool Presente { get; set; }
    }
}
