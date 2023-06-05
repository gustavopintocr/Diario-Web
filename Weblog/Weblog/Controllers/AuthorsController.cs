using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weblog.Data;

namespace Weblog.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly WeblogContext _context;

        public AuthorsController(WeblogContext context)
        {
            _context = context;
        }
        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var weblogContext = _context.Users;
            return View(await weblogContext.ToListAsync());
        }

        //// GET: Authors/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Authors/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Authors/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Authors/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Authors/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Authors/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Authors/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
