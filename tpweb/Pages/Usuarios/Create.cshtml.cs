using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tpweb.Data;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Pages.Usuarios
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Usuario Usuario { get; set; } = default!;

        public List<SelectListItem> RolesSelect { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
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

            _context.Usuarios.Add(Usuario);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }


    }
}
