using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Pages.Usuarios
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Usuario Usuario { get; set; } = default!;

        public List<SelectListItem> RolesSelect { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (Usuario == null) return NotFound();

            RolesSelect = await _context.Roles
                .Select(r => new SelectListItem
                {
                    Value = r.IdRol.ToString(),
                    Text = r.Nombre
                }).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                RolesSelect = await _context.Roles
                    .Select(r => new SelectListItem
                    {
                        Value = r.IdRol.ToString(),
                        Text = r.Nombre
                    }).ToListAsync();

                return Page();
            }

            Usuario.Rol = await _context.Roles.FindAsync(Usuario.Rol.IdRol);

            _context.Attach(Usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuarios.Any(u => u.IdUsuario == Usuario.IdUsuario))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("./Index");
        }



    }
}
