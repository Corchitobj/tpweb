using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using tpweb.Data;
using tpweb.Modelos.Clase_Persona;

namespace tpweb.Pages
{
    public class RegistrarseModel : PageModel
    {
        private readonly AppDbContext _context;

        public RegistrarseModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Usuario NuevoUsuario { get; set; } = new();

        [BindProperty]
        public string Perfil { get; set; } = string.Empty;

        public List<SelectListItem> Perfiles { get; set; } = new();

        public void OnGet()
        {
            // Lista de perfiles que se muestran en el <select>
            Perfiles = new List<SelectListItem>
            {
                new SelectListItem { Value = "Docente", Text = "Docente" },
                new SelectListItem { Value = "Preceptor", Text = "Preceptor" },
                new SelectListItem { Value = "Alumno", Text = "Alumno" }
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Volvemos a cargar la lista para que no se pierda al volver a la vista
                OnGet();
                return Page();
            }

            // Determinar a qué tabla guardar según el perfil seleccionado
            switch (Perfil)
            {
                case "Alumno":
                    var nuevoAlumno = new Alumno
                    {
                        Nombre = NuevoUsuario.Nombre,
                        Apellido = NuevoUsuario.Apellido,
                        Dni = NuevoUsuario.Dni,
                        Mail = NuevoUsuario.Mail,
                        Usuario = NuevoUsuario.UsuarioNombre,
                        Contraseña = NuevoUsuario.Contraseña
                    };
                    _context.Alumnos.Add(nuevoAlumno);
                    break;

                case "Administrador":
                case "Docente":
                case "Preceptor":
                    // Buscamos el rol correspondiente
                    var rol = _context.Roles.FirstOrDefault(r => r.Nombre == Perfil);
                    if (rol == null)
                    {
                        ModelState.AddModelError("", "Rol no encontrado en la base de datos.");
                        OnGet();
                        return Page();
                    }

                    NuevoUsuario.RolId = rol.IdRol;
                    _context.Usuarios.Add(NuevoUsuario);
                    break;

                default:
                    ModelState.AddModelError("", "Debe seleccionar un perfil válido.");
                    OnGet();
                    return Page();
            }

            await _context.SaveChangesAsync();

            // Redirigir a alguna página de éxito o login
            return RedirectToPage("/Index");
        }
    }
}