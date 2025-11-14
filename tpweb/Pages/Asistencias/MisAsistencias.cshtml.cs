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

        [BindProperty(SupportsGet = true)]
        public int? MateriaId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }


        public List<SelectListItem> Materias { get; set; } = new();

        public List<AsistenciaViewModel> Asistencias { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var alumnoId = HttpContext.Session.GetInt32("AlumnoId");

            if (!alumnoId.HasValue)
            {
                Materias = new List<SelectListItem>();
                Asistencias = new List<AsistenciaViewModel>();
                return Page();
            }

            ReturnUrl ??= "/Index";

            // CARGAR MATERIAS
            Materias = await _context.Materias
                .Where(m => m.MateriaAlumnos.Any(ma => ma.AlumnoId == alumnoId.Value))
                .Select(m => new SelectListItem
                {
                    Value = m.IdMateria.ToString(),
                    Text = m.Nombre
                })
                .ToListAsync();

            // QUERY BASE
            var query = _context.AsistenciasAlumnos
                .Include(a => a.Asistencia)
                    .ThenInclude(a => a.Materia)
                .Where(a => a.AlumnoId == alumnoId.Value);

            // FILTRO
            if (MateriaId.HasValue && MateriaId.Value > 0)
            {
                query = query.Where(a => a.Asistencia.MateriaId == MateriaId.Value);
            }

            var lista = await query
                .OrderByDescending(a => a.Asistencia.Fecha)
                .ToListAsync();

            Asistencias = lista.Select(a => new AsistenciaViewModel
            {
                Fecha = a.Asistencia.Fecha,
                Materia = a.Asistencia.Materia?.Nombre ?? "Sin materia",
                Presente = a.Presente
            }).ToList();

            return Page();
        }

        public class AsistenciaViewModel
        {
            public DateTime Fecha { get; set; }
            public string Materia { get; set; } = "";
            public bool Presente { get; set; }
        }
    }
}