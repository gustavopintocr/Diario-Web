using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Weblog.Controllers
{
    public class AdministrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
