using tpweb.Modelos.Clase_Escuela;

namespace tpweb.Modelos.Clase_Persona
{
    public class TareaAlumno
    {
        public int TareaId { get; set; }
        public Tarea Tarea { get; set; } = null!;

        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; } = null!;

        public double? Nota { get; set; }
        public string? Respuesta { get; set; }
        public DateTime? FechaRespuesta { get; set; }
        public bool EstadoEntrega { get; set; }
    }
}
