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
    public class DetailsModel : PageModel
    {
        private readonly tpweb.Data.AppDbContext _context;

        public DetailsModel(tpweb.Data.AppDbContext context)
        {
            _context = context;
        }

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
    }
}
