using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Escuela;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Pages.Asistencias
{
    public class VerModel : PageModel
    {
        private readonly AppDbContext _context;

        public VerModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int MateriaId { get; set; }

        public string MateriaNombre { get; set; } = string.Empty;

        public List<Asistencia> Asistencias { get; set; } = new();
        public List<AsistenciaAlumno> AsistenciasAlumnos { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var materia = await _context.Materias
                .Include(m => m.Curso)
                .FirstOrDefaultAsync(m => m.IdMateria == MateriaId);

            if (materia == null)
                return NotFound();

            MateriaNombre = materia.Nombre;

            Asistencias = await _context.Asistencias
                .Where(a => a.MateriaId == MateriaId)
                .OrderByDescending(a => a.Fecha)
                .ToListAsync();

            AsistenciasAlumnos = await _context.AsistenciasAlumnos
                .Include(aa => aa.Asistencia)
                .ToListAsync();

            return Page();
        }

        public int GetPresentes(int asistenciaId)
        {
            return AsistenciasAlumnos.Count(aa => aa.AsistenciaId == asistenciaId && aa.Presente);
        }

        public int GetTotales(int asistenciaId)
        {
            return AsistenciasAlumnos.Count(aa => aa.AsistenciaId == asistenciaId);
        }
    }
}
