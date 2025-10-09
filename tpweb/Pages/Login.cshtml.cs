using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tpweb.Data;
using Microsoft.EntityFrameworkCore;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string UsuarioNombre { get; set; } = string.Empty;

        [BindProperty]
        public string Contraseña { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(UsuarioNombre) || string.IsNullOrEmpty(Contraseña))
            {
                ErrorMessage = "Debe ingresar usuario y contraseña.";
                return Page();
            }

            // Buscar primero en Usuarios (Docente, Preceptor, Admin)
            var usuario = _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefault(u => u.UsuarioNombre == UsuarioNombre && u.Contraseña == Contraseña);

            if (usuario != null)
            {
                HttpContext.Session.SetString("Rol", usuario.Rol.Nombre);
                HttpContext.Session.SetString("UsuarioNombre", usuario.UsuarioNombre);
                return RedirectToPage("Index");
            }

            // Buscar en Alumnos
            var alumno = _context.Alumnos
                .FirstOrDefault(a => a.Usuario == UsuarioNombre && a.Contraseña == Contraseña);

            if (alumno != null)
            {
                HttpContext.Session.SetString("Rol", "Alumno");
                HttpContext.Session.SetString("UsuarioNombre", alumno.Usuario);
                return RedirectToPage("Index");
            }

            ErrorMessage = "Usuario o contraseña incorrectos.";
            return Page();
        }
    }
}