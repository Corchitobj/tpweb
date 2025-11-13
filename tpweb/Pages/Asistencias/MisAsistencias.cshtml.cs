using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;

namespace tpweb.Pages.Asistencias
{
    public class MisAsistenciasModel : PageModel
    {
        private readonly AppDbContext _context;

        public MisAsistenciasModel(AppDbContext context)
        {
            _context = context;
        }

        // Filtro desde query string
        [BindProperty(SupportsGet = true)]
        public int? MateriaId { get; set; }

        // Lista de materias del alumno para el desplegable
        public List<SelectListItem> Materias { get; set; } = new();

        // Lista de inasistencias a mostrar
        public List<AusenciaViewModel> Ausencias { get; set; } = new();

        // Contadores
        public int TotalAusenciasGlobal { get; set; }
        public int TotalAusenciasMateria { get; set; }

        // Obtiene el id del alumno: primero por sesión, sino por nombre de usuario
        private async Task<int?> GetAlumnoIdAsync()
        {
            var idSesion = HttpContext.Session.GetInt32("AlumnoId");
            if (idSesion.HasValue) return idSesion.Value;

            var usuarioNombre = HttpContext.Session.GetString("UsuarioNombre");
            if (!string.IsNullOrEmpty(usuarioNombre))
            {
                var alumno = await _context.Alumnos.FirstOrDefaultAsync(a => a.Usuario == usuarioNombre);
                if (alumno != null) return alumno.IdAlumno;
            }

            return null;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var alumnoId = await GetAlumnoIdAsync();
            if (!alumnoId.HasValue)
            {
                // Usuario no identificado como alumno: mostrar vacío o redirigir según tu política
                Materias = new List<SelectListItem>();
                Ausencias = new List<AusenciaViewModel>();
                TotalAusenciasGlobal = 0;
                TotalAusenciasMateria = 0;
                return Page();
            }

            // Cargar materias donde el alumno está inscripto
            Materias = await _context.Materias
                .Where(m => m.MateriaAlumnos.Any(ma => ma.AlumnoId == alumnoId.Value))
                .Select(m => new SelectListItem
                {
                    Value = m.IdMateria.ToString(),
                    Text = m.Nombre
                })
                .ToListAsync();

            // Query base: sólo inasistencias del alumno
            var baseQuery = _context.AsistenciasAlumnos
                .Include(aa => aa.Asistencia)
                    .ThenInclude(a => a.Materia)
                .Where(aa => aa.AlumnoId == alumnoId.Value && !aa.Presente);

            TotalAusenciasGlobal = await baseQuery.CountAsync();

            if (MateriaId.HasValue && MateriaId.Value > 0)
            {
                baseQuery = baseQuery.Where(aa => aa.Asistencia.MateriaId == MateriaId.Value);
                TotalAusenciasMateria = await _context.AsistenciasAlumnos
                    .Include(aa => aa.Asistencia)
                    .Where(aa => aa.AlumnoId == alumnoId.Value && !aa.Presente && aa.Asistencia.MateriaId == MateriaId.Value)
                    .CountAsync();
            }
            else
            {
                TotalAusenciasMateria = 0;
            }

            var lista = await baseQuery
                .OrderByDescending(aa => aa.Asistencia.Fecha)
                .ToListAsync();

            Ausencias = lista.Select(aa => new AusenciaViewModel
            {
                Fecha = aa.Asistencia.Fecha,
                Materia = aa.Asistencia.Materia?.Nombre ?? "Sin materia",
                AsistenciaId = aa.AsistenciaId
            }).ToList();

            return Page();
        }

        public class AusenciaViewModel
        {
            public int AsistenciaId { get; set; }
            public DateTime Fecha { get; set; }
            public string Materia { get; set; } = "";
        }
    }
}
