using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Escuela;

namespace tpweb.Pages.Materias
{
    public class EditModel : PageModel
    {
        private readonly tpweb.Data.AppDbContext _context;

        public EditModel(tpweb.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Materia Materia { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia =  await _context.Materias.FirstOrDefaultAsync(m => m.IdMateria == id);
            if (materia == null)
            {
                return NotFound();
            }
            Materia = materia;
           ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre");
           ViewData["DocenteId"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Materia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MateriaExists(Materia.IdMateria))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MateriaExists(int id)
        {
            return _context.Materias.Any(e => e.IdMateria == id);
        }
    }
}
