using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Pages.Tareas
{
    public class CalificarTareaModel : PageModel
    {
        private readonly AppDbContext _context;

        public CalificarTareaModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int TareaId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int AlumnoId { get; set; }

        [BindProperty]
        public double? Nota { get; set; }

        public TareaAlumno? TareaAlumno { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            TareaAlumno = await _context.TareasAlumnos
                .Include(ta => ta.Alumno)
                .Include(ta => ta.Tarea)
                .FirstOrDefaultAsync(ta => ta.TareaId == TareaId && ta.AlumnoId == AlumnoId);

            if (TareaAlumno == null)
            {
                TempData["ErrorMessage"] = "No se encontró la tarea o la respuesta del alumno.";
                return RedirectToPage("/Tareas/Corregir", new { tareaId = TareaId });
            }

            Nota = TareaAlumno.Nota;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (TareaId <= 0 || AlumnoId <= 0)
            {
                TempData["ErrorMessage"] = "Parámetros inválidos.";
                return RedirectToPage("/Tareas/Corregir", new { tareaId = TareaId });
            }

            TareaAlumno = await _context.TareasAlumnos
                .Include(ta => ta.Tarea)
                .FirstOrDefaultAsync(ta => ta.TareaId == TareaId && ta.AlumnoId == AlumnoId);

            if (TareaAlumno == null)
            {
                TempData["ErrorMessage"] = "No se encontró la tarea del alumno.";
                return RedirectToPage("/Tareas/Corregir", new { tareaId = TareaId });
            }

            if (Nota is < 0 or > 10)
            {
                ModelState.AddModelError("Nota", "La calificación debe ser entre 0 y 10.");
                return Page();
            }

            TareaAlumno.Nota = Nota;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Tarea calificada correctamente.";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Error al guardar la calificación. Intente nuevamente.";
            }

            return RedirectToPage("/Tareas/Corregir", new { tareaId = TareaAlumno.TareaId });
        }
    }
}