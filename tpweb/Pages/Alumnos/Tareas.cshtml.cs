using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;

namespace tpweb.Pages.Alumnos
{
    public class TareasModel : PageModel
    {
        private readonly AppDbContext _context;

        public TareasModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int? MateriaId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }

        public List<SelectListItem> Materias { get; set; } = new();
        public List<TareaViewModel> Tareas { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var alumnoId = HttpContext.Session.GetInt32("AlumnoId");

            if (!alumnoId.HasValue)
            {
                Tareas = new();
                Materias = new();
                return Page();
            }

            ReturnUrl ??= "/Index";

            // MATERIAS DEL ALUMNO
            Materias = await _context.Materias
                .Where(m => m.MateriaAlumnos.Any(ma => ma.AlumnoId == alumnoId.Value))
                .Select(m => new SelectListItem
                {
                    Value = m.IdMateria.ToString(),
                    Text = m.Nombre
                })
                .ToListAsync();

            // TRAER TODAS LAS TAREAS NO ARCHIVADAS
            var tareasBase = _context.Tareas
                .Include(t => t.Materia)
                .Where(t => !t.Archivada);

            // FILTRO POR MATERIA
            if (MateriaId.HasValue)
            {
                tareasBase = tareasBase.Where(t => t.MateriaId == MateriaId.Value);
            }

            var tareas = await tareasBase
                .OrderByDescending(t => t.FechaEntrega)
                .ToListAsync();

            // TAREAS DEL ALUMNO
            var tareasAlumno = await _context.TareasAlumnos
                .Where(ta => ta.AlumnoId == alumnoId.Value)
                .ToListAsync();

            Tareas = tareas.Select(t =>
            {
                var relacion = tareasAlumno.FirstOrDefault(ta => ta.TareaId == t.Id);

                return new TareaViewModel
                {
                    TareaId = t.Id,
                    Titulo = t.Titulo,
                    Descripcion = t.Descripcion,
                    FechaEntrega = t.FechaEntrega,
                    Materia = t.Materia.Nombre,

                    // Datos clave:
                    Respuesta = relacion?.Respuesta,
                    EstadoEntrega = relacion?.EstadoEntrega ?? false,
                    Nota = relacion?.Nota,
                    Corregida = relacion?.Nota != null
                };
            }).ToList();

            return Page();
        }

        public class TareaViewModel
        {
            public int TareaId { get; set; }
            public string Titulo { get; set; } = "";
            public string Descripcion { get; set; } = "";
            public DateTime FechaEntrega { get; set; }
            public string Materia { get; set; } = "";

            public string? Respuesta { get; set; }
            public bool EstadoEntrega { get; set; }
            public double? Nota { get; set; }
            public bool Corregida { get; set; }
        }
    }
}
