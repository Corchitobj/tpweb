using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Escuela;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Pages.Asistencias
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

        public List<Alumno> Alumnos { get; set; } = new();
        public Asistencia NuevaAsistencia { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            // Revisar si ya hay asistencia para hoy
            var asistenciaHoy = await _context.Asistencias
                .FirstOrDefaultAsync(a => a.MateriaId == MateriaId && a.Fecha.Date == DateTime.Today);

            if (asistenciaHoy != null)
            {
                // Redirigir a editar asistencia existente
                return RedirectToPage("/Asistencias/Editar", new { id = asistenciaHoy.Id });
            }

            // Traer los alumnos de la materia
            Alumnos = await _context.MateriasAlumnos
                .Where(ma => ma.MateriaId == MateriaId)
                .Select(ma => ma.Alumno)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(List<int> presentes)
        {
            // Crear registro de Asistencia para hoy
            var asistencia = new Asistencia
            {
                Fecha = DateTime.Now,
                MateriaId = MateriaId
            };
            _context.Asistencias.Add(asistencia);
            await _context.SaveChangesAsync();

            // Traer todos los alumnos de la materia
            var alumnos = await _context.MateriasAlumnos
                .Where(ma => ma.MateriaId == MateriaId)
                .Select(ma => ma.Alumno)
                .ToListAsync();

            // Crear registros de AsistenciaAlumno
            foreach (var alumno in alumnos)
            {
                var aa = new AsistenciaAlumno
                {
                    AlumnoId = alumno.IdAlumno,
                    AsistenciaId = asistencia.Id,
                    Presente = presentes.Contains(alumno.IdAlumno)
                };
                _context.AsistenciasAlumnos.Add(aa);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("/Materias/Index");
        }
    }
}
