using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System.Diagnostics;
using Weblog.Data;
using Weblog.Models;

namespace Weblog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WeblogContext _context;

        public HomeController(ILogger<HomeController> logger, WeblogContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Lo meti aca porque en el metodo no me los esta jalando
            //get categories
            List<Category> categories;
            categories = _context.Category != null ? _context.Category.OrderBy(c => c.Name).ToList() : new List<Category>();
            ViewBag.Categories = categories;

            // get publications
            List<Publication> publications;
            publications = _context.Publication != null ? _context.Publication.OrderBy(x => x.Date).ToList(): new List<Publication>();
            ViewBag.Publications = publications;

            var totalCount = ContarRegistrosEnBaseDeDatos();
            ViewBag.PageSize = 5;

            return View();
        }

        //public void getCategories()
        //{
        //    List<Category> categories;
        //    categories = _context.Category != null ? _context.Category.OrderBy(c => c.Name).ToList() : new List<Category>();
        //    ViewBag.Categories = categories;
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Paginación Carga bajo demanda
        public async Task<IPagedList> Pages(int? page)
        {
            int pageSize = 5; // Cantidad de elementos por página
            int pageNumber = (page ?? 1);
            ICollection<Publication> result = await ObtenerDatosPorPagina(pageNumber, pageSize);
            int totalItems = await ObtenerNumeroTotalDeDatos();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Crear un objeto de paginación utilizando los datos obtenidos, el número de página actual y el número total de páginas
            IPagedList<IActionResult> pagedData = new StaticPagedList<IActionResult>((IEnumerable<IActionResult>)(new[] { result }), pageNumber, pageSize, totalPages);

            return pagedData;
        }

        //public async Task<IActionResult> GetPaginatedPosts(int pageNumber, int pageSize)
        //{
        //    var paginatedPosts = await ObtenerDatosPorPagina(pageNumber, pageSize);
        //    return PartialView("_PaginatedPosts", paginatedPosts);
        //}

        private async Task<ICollection<Publication>> ObtenerDatosPorPagina(int pageNumber, int pageSize)
        {
            int startIndex = (pageNumber - 1) * pageSize;
            var result = await _context.Publication
                .OrderBy(x => x.Date)  // Ordenar por fecha
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            return result;
        }


        private async Task<int> ObtenerNumeroTotalDeDatos()
        {
            int totalItems = await ContarRegistrosEnBaseDeDatos();

            return totalItems;
        }

        private async Task<int> ContarRegistrosEnBaseDeDatos()
        {
            int totalItems = await _context.Publication.CountAsync();
            ViewBag.TotalItems = totalItems;
            ViewBag.PageSize = 5;
            return totalItems;
        }
    }
}