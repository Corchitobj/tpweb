using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Escuela;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Pages.Materias
{
    public class AlumnosModel : PageModel
    {
        private readonly AppDbContext _context;

        public AlumnosModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Alumno> Alumnos { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int MateriaId { get; set; }

        public async Task OnGetAsync()
        {
            // Obtener alumnos vinculados a la materia
            Alumnos = await _context.MateriasAlumnos
                .Where(ma => ma.MateriaId == MateriaId)
                .Include(ma => ma.Alumno)
                .Select(ma => ma.Alumno)
                .ToListAsync();
        }
    }
}
