using Microsoft.AspNetCore.Mvc.Rendering;

namespace tpweb.Modelos.Clase_Persona
{
    public class AsistenciaAlumnoViewModel
    {
        public int MateriaIdSeleccionada { get; set; }
        public List<SelectListItem> Materias { get; set; } = new();
        public List<AsistenciaDetalle> Asistencias { get; set; } = new();
        public int TotalAusencias { get; set; }
    }

    public class AsistenciaDetalle
    {
        public DateTime Fecha { get; set; }
        public string Materia { get; set; } = string.Empty;
        public bool Presente { get; set; }
    }
}

