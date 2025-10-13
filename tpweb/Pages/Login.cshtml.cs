using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using tpweb.Data;
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
        public string Contrase�a { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (!string.IsNullOrEmpty(rol))
            {
                return RedirectToPage("/Dashboard");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(UsuarioNombre) || string.IsNullOrEmpty(Contrase�a))
            {
                ErrorMessage = "Debe ingresar usuario y contrase�a.";
                return Page();
            }

            var usuario = _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefault(u => u.UsuarioNombre == UsuarioNombre && u.Contrase�a == Contrase�a);

            if (usuario != null)
            {
                HttpContext.Session.SetString("Rol", usuario.Rol.Nombre);
                HttpContext.Session.SetString("UsuarioNombre", usuario.UsuarioNombre);
                HttpContext.Session.SetInt32("UsuarioId", usuario.IdUsuario);

                return RedirectToPage("/Dashboard");
            }

            var alumno = _context.Alumnos
                .FirstOrDefault(a => a.Usuario == UsuarioNombre && a.Contrase�a == Contrase�a);

            if (alumno != null)
            {
                HttpContext.Session.SetString("Rol", "Alumno");
                HttpContext.Session.SetString("UsuarioNombre", alumno.Usuario);
                HttpContext.Session.SetInt32("UsuarioId", alumno.IdAlumno);

                return RedirectToPage("/Dashboard");
            }

            ErrorMessage = "Usuario o contrase�a incorrectos.";
            return Page();
        }
    }
}