using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Escuela;

namespace tpweb.Pages.Tareas
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int MateriaId { get; set; }

        public Materia? Materia { get; set; }

        public List<Tarea> Tareas { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Materia = await _context.Materias.FirstOrDefaultAsync(m => m.IdMateria == MateriaId);
            if (Materia == null)
            {
                TempData["ErrorMessage"] = "Materia no encontrada.";
                return RedirectToPage("/Materias/Index");
            }

            Tareas = await _context.Tareas
                .Where(t => t.MateriaId == MateriaId)
                .OrderBy(t => t.FechaEntrega)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostToggleArchiveAsync(int tareaId)
        {
            var tarea = await _context.Tareas.FirstOrDefaultAsync(t => t.Id == tareaId);
            if (tarea == null)
            {
                TempData["ErrorMessage"] = "Tarea no encontrada.";
                return RedirectToPage(new { MateriaId });
            }

            tarea.Archivada = !tarea.Archivada;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = tarea.Archivada
                ? "Tarea archivada correctamente."
                : "Tarea desarchivada correctamente.";

            return RedirectToPage(new { MateriaId });
        }
    }
}
