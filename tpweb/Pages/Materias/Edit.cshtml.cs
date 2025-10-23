using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Escuela;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Pages.Materias
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Materia Materia { get; set; } = default!;

        public List<SelectListItem> CursosSelect { get; set; } = new();
        public List<SelectListItem> DocentesSelect { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Materia = await _context.Materias
                .Include(m => m.Curso)
                .Include(m => m.Docente)
                .FirstOrDefaultAsync(m => m.IdMateria == id);

            if (Materia == null)
                return NotFound();

            CursosSelect = await _context.Cursos
                .OrderBy(c => c.Nivel)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre
                })
                .ToListAsync();

            DocentesSelect = await _context.Usuarios
                .Where(u => u.Rol != null && u.Rol.Nombre == "Docente")
                .OrderBy(u => u.Apellido)
                .Select(d => new SelectListItem
                {
                    Value = d.IdUsuario.ToString(),
                    Text = $"{d.Apellido}, {d.Nombre}"
                })
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Attach(Materia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Materias.Any(e => e.IdMateria == Materia.IdMateria))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
