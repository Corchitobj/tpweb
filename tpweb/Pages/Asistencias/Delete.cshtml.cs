using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Escuela;

namespace tpweb.Pages.Asistencias
{
    public class DeleteModel : PageModel
    {
        private readonly tpweb.Data.AppDbContext _context;

        public DeleteModel(tpweb.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Asistencia Asistencia { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistencia = await _context.Asistencias.FirstOrDefaultAsync(m => m.Id == id);

            if (asistencia == null)
            {
                return NotFound();
            }
            else
            {
                Asistencia = asistencia;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistencia = await _context.Asistencias.FindAsync(id);
            if (asistencia != null)
            {
                Asistencia = asistencia;
                _context.Asistencias.Remove(Asistencia);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
