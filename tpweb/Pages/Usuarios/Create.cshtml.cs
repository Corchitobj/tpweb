using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using tpweb.Data;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Pages.Usuarios
{
    public class CreateModel : PageModel
    {
        private readonly tpweb.Data.AppDbContext _context;

        public CreateModel(tpweb.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["RolId"] = new SelectList(_context.Roles, "IdRol", "IdRol");
            return Page();
        }

        [BindProperty]
        public Usuario Usuario { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Usuarios.Add(Usuario);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
