using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Escuela;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Pages.Tareas
{
    public class CorregirModel : PageModel
    {
        private readonly AppDbContext _context;

        public CorregirModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int TareaId { get; set; }

        public Tarea? Tarea { get; set; }
        public List<TareaAlumno> TareasAlumnos { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Tarea = await _context.Tareas
                .Include(t => t.TareasAlumnos)
                .ThenInclude(ta => ta.Alumno)
                .FirstOrDefaultAsync(t => t.Id == TareaId);

            if (Tarea == null)
            {
                TempData["ErrorMessage"] = "Tarea no encontrada.";
                return RedirectToPage("/Materias/Index");
            }

            TareasAlumnos = Tarea.TareasAlumnos;

            return Page();
        }
    }
}
