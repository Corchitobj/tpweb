using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Escuela;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Pages.Asistencias
{
    public class DetalleModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetalleModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; } // Id de la asistencia

        public Asistencia Asistencia { get; set; } = new();
        public string MateriaNombre { get; set; } = string.Empty;
        public List<AsistenciaAlumno> Alumnos { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Asistencia = await _context.Asistencias
                .Include(a => a.Materia)
                .FirstOrDefaultAsync(a => a.Id == Id);

            if (Asistencia == null)
                return NotFound();

            MateriaNombre = Asistencia.Materia.Nombre;

            Alumnos = await _context.AsistenciasAlumnos
                .Include(aa => aa.Alumno)
                .Where(aa => aa.AsistenciaId == Id)
                .ToListAsync();

            return Page();
        }
    }
}