using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Pages.Alumnos
{
    public class AsistenciasModel : PageModel
    {
        private readonly AppDbContext _context;

        public AsistenciasModel(AppDbContext context)
        {
            _context = context;
        }

        public Alumno Alumno { get; set; } = default!;
        public List<AsistenciaAlumno> AsistenciasDelAlumno { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Alumno = await _context.Alumnos
                .Include(a => a.AsistenciasAlumnos)
                    .ThenInclude(aa => aa.Asistencia)
                        .ThenInclude(a => a.Materia)
                .FirstOrDefaultAsync(a => a.IdAlumno == id);

            if (Alumno == null)
                return NotFound();

            AsistenciasDelAlumno = Alumno.AsistenciasAlumnos
                .OrderByDescending(aa => aa.Asistencia.Fecha)
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostActualizarAsync(List<AsistenciaAlumno> asistenciasActualizadas)
        {
            foreach (var asistencia in asistenciasActualizadas)
            {
                var original = await _context.AsistenciasAlumnos
                    .FirstOrDefaultAsync(aa =>
                        aa.AsistenciaId == asistencia.AsistenciaId &&
                        aa.AlumnoId == asistencia.AlumnoId);

                if (original != null)
                    original.Presente = asistencia.Presente;
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }


    }
}
