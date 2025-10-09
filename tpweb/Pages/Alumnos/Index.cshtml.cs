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
    public class IndexModel : PageModel
    {
        private readonly tpweb.Data.AppDbContext _context;

        public IndexModel(tpweb.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Alumno> Alumno { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Alumno = await _context.Alumnos.ToListAsync();
        }
    }
}
