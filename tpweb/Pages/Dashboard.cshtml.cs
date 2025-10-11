using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace tpweb.Pages
{
    public class DashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Administrador" && rol != "Preceptor" && rol != "Docente")
            {
                return RedirectToPage("/Login");
            }
            return Page();
        }

        

    }
}