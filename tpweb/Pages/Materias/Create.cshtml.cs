using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tpweb.Data;
using tpweb.Modelos.Clase_Escuela;

namespace tpweb.Pages.Materias
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Materia Materia { get; set; } = default!;

        public List<SelectListItem> CursosSelect { get; set; } = new();
        public List<SelectListItem> DocentesSelect { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            CursosSelect = await _context.Cursos
                .OrderBy(c => c.Nivel)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre
                }).ToListAsync();

            DocentesSelect = await _context.Usuarios
                .Where(u => u.Rol != null && u.Rol.Nombre == "Docente")
                .OrderBy(u => u.Apellido)
                .Select(d => new SelectListItem
                {
                    Value = d.IdUsuario.ToString(),
                    Text = $"{d.Apellido}, {d.Nombre}"
                }).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Materias.Add(Materia);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }

}
