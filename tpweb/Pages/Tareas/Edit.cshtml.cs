using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Escuela;

namespace tpweb.Pages.Tareas
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tarea Tarea { get; set; } = new();

        public Materia? Materia { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Tarea = await _context.Tareas
                .Include(t => t.Materia)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (Tarea == null)
            {
                TempData["ErrorMessage"] = "Tarea no encontrada.";
                return RedirectToPage("/Materias/Index");
            }

            Materia = Tarea.Materia;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Remover validación de navegación
            ModelState.Remove("Tarea.Materia");

            if (!ModelState.IsValid)
            {
                Materia = await _context.Materias
                    .FirstOrDefaultAsync(m => m.IdMateria == Tarea.MateriaId);
                return Page();
            }

            var tareaExistente = await _context.Tareas
                .FirstOrDefaultAsync(t => t.Id == Tarea.Id);

            if (tareaExistente == null)
            {
                TempData["ErrorMessage"] = "La tarea que intenta editar no existe.";
                return RedirectToPage("/Materias/Index");
            }

            // Actualizar los campos editables
            tareaExistente.Titulo = Tarea.Titulo;
            tareaExistente.Descripcion = Tarea.Descripcion;
            tareaExistente.FechaEntrega = Tarea.FechaEntrega;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Tarea actualizada correctamente.";
                return RedirectToPage("/Tareas/Index", new { materiaId = tareaExistente.MateriaId });
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Error al actualizar la tarea.");
                Materia = await _context.Materias
                    .FirstOrDefaultAsync(m => m.IdMateria == Tarea.MateriaId);
                return Page();
            }
        }
    }
}
