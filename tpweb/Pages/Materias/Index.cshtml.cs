using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Escuela;

namespace tpweb.Pages.Materias
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        // Filtro por curso (desde el formulario)
        [BindProperty(SupportsGet = true)]
        public int? CursoId { get; set; }

        // Lista de materias a mostrar
        public IList<Materia> Materia { get; set; } = new List<Materia>();

        // Lista de cursos para el desplegable
        public List<Curso> Cursos { get; set; } = new();

        public async Task OnGetAsync()
        {
            var rol = HttpContext.Session.GetString("Rol");
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            // Cargar cursos ordenados por nivel (1 = Primer año, 2 = Segundo, etc.)
            Cursos = await _context.Cursos
                .OrderBy(c => c.Nivel)
                .ToListAsync();

            // Construir consulta base
            var query = _context.Materias
                .Include(m => m.Curso)
                .Include(m => m.Docente)
                .AsQueryable();

            // Filtrar según rol
            if (rol == "Docente" && usuarioId.HasValue)
            {
                query = query.Where(m => m.DocenteId == usuarioId.Value);
            }
            else if (rol == "Administrador" && usuarioId.HasValue)
            {
                if (CursoId.HasValue && CursoId.Value > 0)
                {
                    query = query.Where(m => m.CursoId == CursoId.Value);
                }
            }

            // Ejecutar consulta
            Materia = await query.ToListAsync();
        }
    }


}
