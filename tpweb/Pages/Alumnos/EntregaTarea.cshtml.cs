using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Persona;
using tpweb.Modelos.Clase_Escuela;

namespace tpweb.Pages.Alumnos
{
    public class EntregarTareaModel : PageModel
    {
        private readonly AppDbContext _context;

        public EntregarTareaModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }   // ID de la tarea

        [BindProperty]
        public string Respuesta { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }

        public Tarea? Tarea { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var alumnoId = HttpContext.Session.GetInt32("AlumnoId");
            if (!alumnoId.HasValue)
                return RedirectToPage("/Login");

            ReturnUrl ??= "/Alumnos/Tareas";

            Tarea = await _context.Tareas
                .Include(t => t.Materia)
                .FirstOrDefaultAsync(t => t.Id == Id);

            if (Tarea == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var alumnoId = HttpContext.Session.GetInt32("AlumnoId");
            if (!alumnoId.HasValue)
                return RedirectToPage("/Login");

            var tarea = await _context.Tareas.FindAsync(Id);
            if (tarea == null)
                return NotFound();

            // Buscar si ya existe la relación
            var registro = await _context.TareasAlumnos
                .FirstOrDefaultAsync(ta => ta.TareaId == Id && ta.AlumnoId == alumnoId.Value);

            if (registro == null)
            {
                // Crear relación nueva
                registro = new TareaAlumno
                {
                    TareaId = Id,
                    AlumnoId = alumnoId.Value,
                    Respuesta = Respuesta,
                    FechaRespuesta = DateTime.Now,
                    EstadoEntrega = true
                };
                _context.TareasAlumnos.Add(registro);
            }
            else
            {
                // Actualizar relación existente
                registro.Respuesta = Respuesta;
                registro.FechaRespuesta = DateTime.Now;
                registro.EstadoEntrega = true;
            }

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Tarea enviada correctamente.";

            return Redirect(ReturnUrl ?? "/Alumnos/Tareas");
        }
    }
}
