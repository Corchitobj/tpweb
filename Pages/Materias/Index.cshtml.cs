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
    public class IndexModel : PageModel
    {
        private readonly tpweb.Data.AppDbContext _context;

        public IndexModel(tpweb.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Materia> Materia { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Materia = await _context.Materias
                .Include(m => m.Curso)
                .Include(m => m.Docente).ToListAsync();
        }
    }
}
