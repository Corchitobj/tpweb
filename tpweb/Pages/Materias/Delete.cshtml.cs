using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Escuela;

namespace tpweb.Pages.Materias
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Materia Materia { get; set; } = default!;

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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var materia = await _context.Materias.FindAsync(id);
            if (materia != null)
            {
                Materia = materia;
                _context.Materias.Remove(Materia);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }


}
