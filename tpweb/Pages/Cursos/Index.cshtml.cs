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
                        .Where(a =>
                            // Verificamos que el alumno no esté vinculado a ninguna materia
                            !a.MateriaAlumnos.Any() ||
                            // O solo está vinculado a materias de este curso
                            a.MateriaAlumnos.All(ma => ma.Materia.CursoId == c.Id)
                        )
                        .Where(a => !a.MateriaAlumnos.Any(ma => ma.Materia.CursoId == c.Id))
                        .ToList()
                })
                .ToList();
        }

        public async Task<IActionResult> OnPostVincularAlumnoAsync(int cursoId, int alumnoId)
        {
            // Obtener todas las materias del curso
            var materiasDelCurso = await _context.Materias
                .Where(m => m.CursoId == cursoId)
                .ToListAsync();

            if (!materiasDelCurso.Any())
                return NotFound("No hay materias asociadas a este curso.");

            // Obtener los IDs de las materias del curso
            var idsMateriasDelCurso = materiasDelCurso.Select(m => m.IdMateria).ToList();

            // Obtener las vinculaciones existentes del alumno para esas materias
            var materiasYaVinculadas = await _context.MateriasAlumnos
                .Where(ma => ma.AlumnoId == alumnoId && idsMateriasDelCurso.Contains(ma.MateriaId))
                .Select(ma => ma.MateriaId)
                .ToListAsync();

            // Crear las vinculaciones que faltan
            var nuevasVinculaciones = idsMateriasDelCurso
                .Where(id => !materiasYaVinculadas.Contains(id))
                .Select(id => new MateriaAlumno
                {
                    AlumnoId = alumnoId,
                    MateriaId = id
                })
                .ToList();

            if (nuevasVinculaciones.Any())
            {
                _context.MateriasAlumnos.AddRange(nuevasVinculaciones);
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