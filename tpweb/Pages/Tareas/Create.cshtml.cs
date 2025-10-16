using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Escuela;

namespace tpweb.Pages.Tareas
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int MateriaId { get; set; }

        [BindProperty]
        public Tarea Tarea { get; set; } = new();

        public Materia? Materia { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (MateriaId <= 0)
            {
                TempData["ErrorMessage"] = "ID de materia inválido.";
                return RedirectToPage("/Materias/Index");
            }

            Materia = await _context.Materias
                .FirstOrDefaultAsync(m => m.IdMateria == MateriaId);

            if (Materia == null)
            {
                TempData["ErrorMessage"] = "Materia no encontrada.";
                return RedirectToPage("/Materias/Index");
            }

            // Inicializamos la tarea con valores por defecto
            Tarea.MateriaId = MateriaId;
            Tarea.FechaEntrega = DateTime.Today;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Removemos la validación de Materia ya que es una propiedad de navegación
            ModelState.Remove("Tarea.Materia");

            // Validación de que la fecha de entrega no puede ser en el pasado
            if (Tarea.FechaEntrega < DateTime.Today)
            {
                ModelState.AddModelError("Tarea.FechaEntrega",
                    "La fecha de entrega no puede ser anterior a hoy.");
            }

            if (!ModelState.IsValid)
            {
                // Recargamos la materia para mostrar el nombre en caso de error
                Materia = await _context.Materias
                    .FirstOrDefaultAsync(m => m.IdMateria == MateriaId);
                return Page();
            }

            // Verificamos que la materia existe
            var materiaExiste = await _context.Materias
                .AnyAsync(m => m.IdMateria == MateriaId);

            if (!materiaExiste)
            {
                TempData["ErrorMessage"] = "La materia especificada no existe.";
                return RedirectToPage("/Materias/Index");
            }

            // Aseguramos la relación correcta
            Tarea.MateriaId = MateriaId;
            Tarea.Materia = null!; // No asignamos la navegación, solo el FK

            try
            {
                _context.Tareas.Add(Tarea);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Tarea creada correctamente.";
                return RedirectToPage("/Materias/Details", new { id = MateriaId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,
                    "Error al guardar la tarea. Por favor, intente nuevamente.");

                Materia = await _context.Materias
                    .FirstOrDefaultAsync(m => m.IdMateria == MateriaId);

                return Page();
            }
        }
    }
}
