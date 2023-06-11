using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using Weblog.Data;
using Weblog.Models;

namespace Weblog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WeblogContext _context;
        private Homepage homepage = new Homepage();

        public HomeController(ILogger<HomeController> logger, WeblogContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            homepage.Publications = _context.Publication.OrderBy(x => x.Date).ToList();
            homepage.Categories = _context.Category.OrderBy(c => c.Name).ToList();
            homepage.Authors = _context.Users
                                        .Join(_context.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { User = u, UserRole = ur })
                                        .Join(_context.Roles, ur => ur.UserRole.RoleId, r => r.Id, (ur, r) => new { ur.User, Role = r })
                                        .Where(x => x.Role.Name == "Author")
                                        .Select(x => x.User)
                                        .ToList();
            return View(homepage);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Método para cargar los resultados paginados mediante Ajax
        public async Task<IActionResult> Pages(int? page)
        {
            int pageSize = 5; // Cantidad de elementos por página
            int pageNumber = page ?? 1;
            var result = await ObtenerDatosPorPagina(pageNumber, pageSize);
            int totalItems = await ContarRegistrosEnBaseDeDatos();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            homepage.Publications = result;
            ViewBag.CurrentPage = pageNumber;
            return PartialView("_PublicationList", homepage.Publications);
        }


        private async Task<List<Publication>> ObtenerDatosPorPagina(int pageNumber, int pageSize)
        {
            int startIndex = (pageNumber - 1) * pageSize;
            List<Publication> result = await _context.Publication
                .OrderBy(x => x.Date)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            return result;
        }

        private async Task<int> ContarRegistrosEnBaseDeDatos()
        {
            return await _context.Publication.CountAsync();
        }
    }
}