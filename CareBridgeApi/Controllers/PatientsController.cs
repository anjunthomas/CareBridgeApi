using Microsoft.AspNetCore.Mvc;

namespace CareBridgeApi.Controllers
{
    public class PatientsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
