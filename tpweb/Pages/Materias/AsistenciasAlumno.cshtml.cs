using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tpweb.Data;
using tpweb.Modelos.Clase_Persona;
using Microsoft.EntityFrameworkCore;

namespace tpweb.Pages.Materias
{
    public class AsistenciasAlumnoModel : PageModel
    {
        private readonly AppDbContext _context;

        public AsistenciasAlumnoModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<AsistenciaAlumno> AsistenciasAlumno { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int AlumnoId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int MateriaId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            AsistenciasAlumno = await _context.AsistenciasAlumnos
                .Include(aa => aa.Asistencia)
                .Where(aa => aa.AlumnoId == AlumnoId && aa.Asistencia.MateriaId == MateriaId)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostModificarAsync(int AsistenciaId, int AlumnoId, int MateriaId)
        {
            var registro = await _context.AsistenciasAlumnos
                .Include(aa => aa.Asistencia)
                .FirstOrDefaultAsync(aa => aa.AsistenciaId == AsistenciaId && aa.AlumnoId == AlumnoId);

            if (registro == null)
            {
                return NotFound();
            }

            registro.Presente = !registro.Presente;
            await _context.SaveChangesAsync();

            AsistenciasAlumno = await _context.AsistenciasAlumnos
                .Include(aa => aa.Asistencia)
                .Where(aa => aa.AlumnoId == AlumnoId && aa.Asistencia.MateriaId == MateriaId)
                .ToListAsync();

            return Page();
        }


    }
}
