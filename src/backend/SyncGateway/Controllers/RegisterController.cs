using Microsoft.AspNetCore.Mvc;

namespace SyncGateway.Controllers
{
    public class RegisterController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}