using System.ComponentModel.DataAnnotations;
using System.Drawing.Drawing2D;

namespace tpweb.Modelos.Clase_Escuela
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public int Nivel { get; set; }
        public int EscuelaId { get; set; }
        public Escuela Escuela { get; set; } = null!;

        public List<Materia> Materias { get; set; } = new();
    }
}
