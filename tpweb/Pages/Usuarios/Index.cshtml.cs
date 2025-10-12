using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Pages.Usuarios
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int? RolId { get; set; } // Filtro por tipo de usuario

        public List<Usuario> Usuario { get; set; }
        public List<Rol> Roles { get; set; }

        public async Task OnGetAsync()
        {
            Roles = await _context.Roles.ToListAsync();

            var query = _context.Usuarios.Include(u => u.Rol).AsQueryable();

            if (RolId.HasValue && RolId.Value > 0)
            {
                query = query.Where(u => u.RolId == RolId.Value);
            }

            Usuario = await query.ToListAsync();
        }

    }
}
