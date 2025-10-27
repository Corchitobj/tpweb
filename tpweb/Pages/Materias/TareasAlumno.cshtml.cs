using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using tpweb.Data;
using tpweb.Modelos.Clase_Persona;
using Microsoft.EntityFrameworkCore;

namespace tpweb.Pages.Materias
{
    public class TareasAlumnoModel : PageModel
    {
        private readonly AppDbContext _context;

        public TareasAlumnoModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<TareaAlumno> TareasAlumno { get; set; } = default!;
        public double Promedio { get; set; }

        [BindProperty(SupportsGet = true)]
        public int AlumnoId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int MateriaId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            TareasAlumno = await _context.TareasAlumnos
                .Include(ta => ta.Tarea)
                .Where(ta => ta.AlumnoId == AlumnoId && ta.Tarea.MateriaId == MateriaId)
                .ToListAsync();

            var notas = TareasAlumno
                .Where(ta => ta.Nota.HasValue)
                .Select(ta => ta.Nota!.Value)
                .ToList();

            Promedio = notas.Any() ? Math.Round(notas.Average(), 2) : 0;

            return Page();
        }

        public async Task<IActionResult> OnPostCorregirAsync(int TareaId, int AlumnoId, double Nota)
        {
            var tareaAlumno = await _context.TareasAlumnos
         .FirstOrDefaultAsync(ta => ta.TareaId == TareaId && ta.AlumnoId == AlumnoId);

            if (tareaAlumno == null)
            {
                return NotFound();
            }

            tareaAlumno.Nota = Nota;
            await _context.SaveChangesAsync();

            // Recargar datos
            TareasAlumno = await _context.TareasAlumnos
                .Include(ta => ta.Tarea)
                .Where(ta => ta.AlumnoId == AlumnoId && ta.Tarea.MateriaId == MateriaId)
                .ToListAsync();

            var notas = TareasAlumno
                .Where(ta => ta.Nota.HasValue)
                .Select(ta => ta.Nota!.Value)
                .ToList();

            Promedio = notas.Any() ? Math.Round(notas.Average(), 2) : 0;

            return Page();


        }

        public async Task<IActionResult> OnPostEditarNotaAsync(int TareaId, int AlumnoId, double Nota)
        {
            var tareaAlumno = await _context.TareasAlumnos
        .FirstOrDefaultAsync(ta => ta.TareaId == TareaId && ta.AlumnoId == AlumnoId);

            if (tareaAlumno == null)
            {
                return NotFound();
            }

            tareaAlumno.Nota = Nota;
            await _context.SaveChangesAsync();

            TareasAlumno = await _context.TareasAlumnos
                .Include(ta => ta.Tarea)
                .Where(ta => ta.AlumnoId == AlumnoId && ta.Tarea.MateriaId == MateriaId)
                .ToListAsync();

            var notas = TareasAlumno
                .Where(ta => ta.Nota.HasValue)
                .Select(ta => ta.Nota!.Value)
                .ToList();

            Promedio = notas.Any() ? Math.Round(notas.Average(), 2) : 0;

            return Page();



        }



    }
}
