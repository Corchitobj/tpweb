using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Pages.Alumnos
{
    public class DetailsModel : PageModel
    {
        private readonly tpweb.Data.AppDbContext _context;

        public DetailsModel(tpweb.Data.AppDbContext context)
        {
            _context = context;
        }

        public Alumno Alumno { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos.FirstOrDefaultAsync(m => m.IdAlumno == id);
            if (alumno == null)
            {
                return NotFound();
            }
            else
            {
                Alumno = alumno;
            }
            return Page();
        }
    }
}
