using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using tpweb.Data;
using tpweb.Modelos.Clase_Escuela;

namespace tpweb.Pages.Materias
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
        ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre");
        ViewData["DocenteId"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido");
            return Page();
        }

        [BindProperty]
        public Materia Materia { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Materias.Add(Materia);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
