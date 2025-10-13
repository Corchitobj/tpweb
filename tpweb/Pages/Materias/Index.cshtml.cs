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

        public IList<Materia> Materia { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var rol = HttpContext.Session.GetString("Rol");
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (rol == "Docente" && usuarioId.HasValue)
            {
                Materia = await _context.Materias
                    .Include(m => m.Curso)
                    .Include(m => m.Docente)
                    .Where(m => m.DocenteId == usuarioId.Value)
                    .ToListAsync();
            }
            else if (rol == "Administrador" && usuarioId.HasValue)
            {
                Materia = await _context.Materias
                    .Include(m => m.Curso)
                    .Include(m => m.Docente)
                    .ToListAsync();
            }
        }
    }
}