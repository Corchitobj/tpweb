using System.ComponentModel.DataAnnotations;

namespace tpweb.Modelos.Clase_Persona
{
    public class Alumno
    {
        [Key]
        public int IdAlumno { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;

        public List<MateriaAlumno> MateriaAlumnos { get; set; } = new();
        public List<AsistenciaAlumno> AsistenciasAlumnos { get; set; } = new();
        public List<TareaAlumno> TareasAlumnos { get; set; } = new();
    }
}
