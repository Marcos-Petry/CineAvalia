using CineAvalia.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CineAvalia.Controllers
{
    [PaginaUsuarioLogado]
    public class RestritoAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
