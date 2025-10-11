using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using tpweb.Data;
using System.Collections.Generic;
using System.Linq;

namespace tpweb.Pages
{
    public class FormulariosModel : PageModel
    {
        private readonly AppDbContext _context;

        public FormulariosModel(AppDbContext context)
        {
            _context = context;
        }

        // Listas para los selects
        public List<SelectListItem> MateriasSelectList { get; set; } = new();
        public List<SelectListItem> CursosSelectList { get; set; } = new();
        public List<SelectListItem> DocentesSelectList { get; set; } = new();

        public void OnGet()
        {
            MateriasSelectList = _context.Materias
                .Select(m => new SelectListItem
                {
                    Value = m.IdMateria.ToString(),
                    Text = m.Nombre
                })
                .ToList();

            CursosSelectList = _context.Cursos
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre
                })
                .ToList();

            DocentesSelectList = _context.Usuarios
                .Where(u => u.Rol != null && u.Rol.Nombre == "Docente")
                .Select(d => new SelectListItem
                {
                    Value = d.IdUsuario.ToString(),
                    Text = d.Nombre
                    
                })
                .ToList();
        }
    }
}