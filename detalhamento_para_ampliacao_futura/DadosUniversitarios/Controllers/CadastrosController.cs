using Microsoft.AspNetCore.Mvc;

namespace DadosUniversitarios.Controllers
{
    public class CadastrosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
