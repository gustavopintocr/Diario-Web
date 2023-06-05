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
        public ActionResult Publications()
        {
            return RedirectToAction("Index", "Publications");
        }
        public ActionResult Comments()
        {
            return RedirectToAction("Index", "Comments");
        }
        //public ActionResult Categories()
        //{
        //    return RedirectToAction("Index", "Categories");
        //}
        public ActionResult Authors()
        {
            return RedirectToAction("Index", "Authors");
        }
    }
}
