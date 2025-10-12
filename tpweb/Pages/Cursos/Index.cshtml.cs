using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Escuela;
using tpweb.Modelos.Clase_Persona;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tpweb.Pages.Cursos
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<CursoViewModel> Cursos { get; set; } = new();

        public void OnGet()
        {
            Cursos = _context.Cursos
                .Include(c => c.Materias)
                    .ThenInclude(m => m.Docente)
                .OrderBy(c => c.Id)
                .Select(c => new CursoViewModel
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Alumnos = _context.Alumnos
                        .Where(a => a.MateriaAlumnos.Any(ma => ma.Materia.CursoId == c.Id))
                        .ToList(),
                    Materias = c.Materias.ToList(),
                    AlumnosNoVinculados = _context.Alumnos
                        .Where(a => !a.MateriaAlumnos.Any(ma => ma.Materia.CursoId == c.Id))
                        .ToList()
                })
                .ToList();
        }

        public async Task<IActionResult> OnPostVincularAlumnoAsync(int cursoId, int alumnoId)
        {
            // Buscar una materia cualquiera del curso (puedes ajustar la lógica si tienes varias materias por curso)
            var materia = await _context.Materias.FirstOrDefaultAsync(m => m.CursoId == cursoId);
            if (materia == null)
                return NotFound();

            var existe = await _context.MateriasAlumnos
                .AnyAsync(ma => ma.MateriaId == materia.IdMateria && ma.AlumnoId == alumnoId);

            if (!existe)
            {
                _context.MateriasAlumnos.Add(new MateriaAlumno
                {
                    MateriaId = materia.IdMateria,
                    AlumnoId = alumnoId
                });
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public class CursoViewModel
        {
            public int Id { get; set; }
            public string Nombre { get; set; } = "";
            public List<Alumno> Alumnos { get; set; } = new();
            public List<Materia> Materias { get; set; } = new();
            public List<Alumno> AlumnosNoVinculados { get; set; } = new();
        }
    }
}