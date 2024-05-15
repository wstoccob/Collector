using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Collector.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
