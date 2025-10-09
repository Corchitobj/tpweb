using Microsoft.EntityFrameworkCore;
using tpweb.Data;

namespace tpweb.Servicios.ServiciosAdministrador
{
    public class srvCurso
    {
        private readonly AppDbContext _context;

        public srvCurso(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los cursos con el nombre de la escuela
        public async Task<List<CursoDTO>> ObtenerCursosAsync()
        {
            return await _context.Cursos
                .Include(c => c.Escuela)
                .Select(c => new CursoDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Escuela = c.Escuela.Nombre
                })
                .ToListAsync();
        }
    }

    // DTO para exponer solo los datos necesarios
    public class CursoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Escuela { get; set; }
    }
}

