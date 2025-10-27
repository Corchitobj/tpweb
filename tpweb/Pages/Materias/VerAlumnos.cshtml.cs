using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
using tpweb.Modelos.Clase_Escuela;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Pages.Materias
{
    public class AlumnosModel : PageModel
    {
        private readonly AppDbContext _context;

        public AlumnosModel(AppDbContext context)
        {
            _context = context;
        }

        public List<AlumnoConPromedio> AlumnosConPromedio { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int MateriaId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Filtro { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var alumnos = await _context.MateriasAlumnos
                .Where(ma => ma.MateriaId == MateriaId)
                .Include(ma => ma.Alumno)
                .Select(ma => ma.Alumno)
                .ToListAsync();

            if (!string.IsNullOrWhiteSpace(Filtro))
            {
                alumnos = alumnos
                    .Where(a =>
                        a.Nombre.Contains(Filtro, StringComparison.OrdinalIgnoreCase) ||
                        a.Apellido.Contains(Filtro, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            foreach (var alumno in alumnos)
            {
                var notas = await _context.TareasAlumnos
                    .Where(ta => ta.AlumnoId == alumno.IdAlumno && ta.Tarea.MateriaId == MateriaId && ta.Nota.HasValue)
                    .Select(ta => ta.Nota!.Value)
                    .ToListAsync();

                double? promedio = notas.Any() ? Math.Round(notas.Average(), 2) : null;

                AlumnosConPromedio.Add(new AlumnoConPromedio
                {
                    Alumno = alumno,
                    Promedio = promedio
                });
            }

            return Page();
        }
    }

    public class AlumnoConPromedio
    {
        public Alumno Alumno { get; set; } = default!;
        public double? Promedio { get; set; }
    }


}
