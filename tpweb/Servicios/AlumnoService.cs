using Microsoft.EntityFrameworkCore;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Servicios
{
    public class AlumnoService
    {

        private readonly tpweb.Data.AppDbContext _context;

        public AlumnoService(tpweb.Data.AppDbContext context)
        {
            _context = context;
        }
        public async Task<Alumno> AltaAlumno(Alumno alumno)
        {
            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();
            return alumno;

        }

    }
}
