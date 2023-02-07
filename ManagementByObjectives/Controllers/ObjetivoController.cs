using Microsoft.AspNetCore.Mvc;

namespace ManagementByObjectives.Controllers
{
    public class ObjetivoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
