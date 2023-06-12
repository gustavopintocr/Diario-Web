using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using Weblog.Data;
using Weblog.Models;
using Weblog.Models.DTOs;
using X.PagedList;

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

        public async Task<IActionResult> Index()
        {
            var categories = _context.Category.OrderBy(c => c.Name);
            homepage.Categories = await categories.ToListAsync();
            var authors = _context.Users
                                        .Join(_context.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { User = u, UserRole = ur })
                                        .Join(_context.Roles, ur => ur.UserRole.RoleId, r => r.Id, (ur, r) => new { ur.User, Role = r })
                                        .Where(x => x.Role.Name == "Author")
                                        .Select(x => x.User)
                                        .ToList();
            homepage.Authors = await authors.ToListAsync();
            return View(homepage);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Pages(int? page)
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;
            var result = await ObtenerDatosPorPagina(pageNumber, pageSize);
            int totalItems = await ContarRegistrosEnBaseDeDatos();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            homepage.Publications = result;
            ViewBag.CurrentPage = pageNumber;
            return PartialView("_PublicationList", homepage.Publications);
        }


        private async Task<List<PublicationWithAuthorInfo>> ObtenerDatosPorPagina(int pageNumber, int pageSize)
        {
            int startIndex = (pageNumber - 1) * pageSize;

            var result = await _context.Publication
                .Include(c => c.User)
                .OrderBy(x => x.Date)
                .Skip(startIndex)
                .Take(pageSize)
                .Select(publication => new PublicationWithAuthorInfo
                {
                    Id = publication.Id,
                    Title = publication.Title,
                    Date = publication.Date,
                    Image = publication.Image,
                    Body = publication.Body,
                    AuthorId = publication.User.Id,
                    AuthorName = publication.User.UserName
                })
                .ToListAsync();

            return result;
        }

        private async Task<int> ContarRegistrosEnBaseDeDatos()
        {
            return await _context.Publication.CountAsync();
        }
    }
}