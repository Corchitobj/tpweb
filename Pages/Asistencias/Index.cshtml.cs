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
    public class IndexModel : PageModel
    {
        private readonly tpweb.Data.AppDbContext _context;

        public IndexModel(tpweb.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Asistencia> Asistencia { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Asistencia = await _context.Asistencias
                .Include(a => a.Materia).ToListAsync();
        }
    }
}
