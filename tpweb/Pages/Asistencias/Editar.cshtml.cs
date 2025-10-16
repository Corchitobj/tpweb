using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Pages.Asistencias
{
    public class EditarModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditarModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; } // Id de Asistencia

        public string MateriaNombre { get; set; } = string.Empty;

        public List<AsistenciaAlumno> AsistenciasAlumnos { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var asistencia = await _context.Asistencias
                .Include(a => a.AsistenciasAlumnos)
                    .ThenInclude(aa => aa.Alumno)
                .Include(a => a.Materia)
                .FirstOrDefaultAsync(a => a.Id == Id);

            if (asistencia == null)
                return NotFound();

            MateriaNombre = asistencia.Materia.Nombre;
            AsistenciasAlumnos = asistencia.AsistenciasAlumnos.ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(List<int> presentes)
        {
            var asistencia = await _context.Asistencias
                .Include(a => a.AsistenciasAlumnos)
                .FirstOrDefaultAsync(a => a.Id == Id);

            if (asistencia == null)
                return NotFound();

            foreach (var aa in asistencia.AsistenciasAlumnos)
            {
                aa.Presente = presentes.Contains(aa.AlumnoId);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("/Materias/Index");
        }
    }
}

