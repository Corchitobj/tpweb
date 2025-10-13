using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Escuela;

namespace tpweb.Pages.MatAlumnos
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Materia> Materias { get; set; } = new List<Materia>();

        public async Task OnGetAsync()
        {
            var alumnoId = HttpContext.Session.GetInt32("AlumnoId");

            if (!alumnoId.HasValue)
            {
                Materias = new List<Materia>();
                return;
            }

            Materias = await _context.Materias
                .Include(m => m.Curso)
                .Include(m => m.Docente)
                .Where(m => m.MateriaAlumnos.Any(ma => ma.AlumnoId == alumnoId.Value))
                .ToListAsync();
        }
    }
}