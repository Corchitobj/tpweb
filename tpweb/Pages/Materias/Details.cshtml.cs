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
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

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
    }


}
