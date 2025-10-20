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

        [BindProperty(SupportsGet = true)]
        public string? FiltroTexto { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? FiltroCurso { get; set; }

        public IList<Alumno> Alumno { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var query = _context.Alumnos
                .Include(a => a.MateriaAlumnos)
                    .ThenInclude(ma => ma.Materia)
                        .ThenInclude(m => m.Curso)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(FiltroTexto))
            {
                query = query.Where(a =>
                    a.Nombre.Contains(FiltroTexto) ||
                    a.Apellido.Contains(FiltroTexto));
            }

            if (!string.IsNullOrWhiteSpace(FiltroCurso))
            {
                query = query.Where(a =>
                    a.MateriaAlumnos.Any(ma =>
                        ma.Materia != null &&
                        ma.Materia.Curso != null &&
                        ma.Materia.Curso.Nombre == FiltroCurso));
            }

            Alumno = await query
                .OrderBy(a => a.Apellido)
                .ToListAsync();
        }
    }


}
