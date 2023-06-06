using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Weblog.Data;
using Weblog.Models;
using PagedList;

namespace Weblog.Controllers
{
    public class PublicationsController : Controller
    {
        private readonly WeblogContext _context;

        public PublicationsController(WeblogContext context)
        {
            _context = context;
        }

        // GET: Publications
        public async Task<IActionResult> Index()
        {
              return _context.Publication != null ? 
                          View(await _context.Publication.Include(c => c.User).ToListAsync()) :
                          Problem("Entity set 'WeblogContext.Publication'  is null.");
        }

        // GET: Publications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Publication == null)
            {
                return NotFound();
            }

            var publication = await _context.Publication
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publication == null)
            {
                return NotFound();
            }

            return View(publication);
        }

        // GET: Publications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Image,Body,UserId")] Publication publication)
        {
            publication.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            publication.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(publication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publication);
        }

        // GET: Publications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Publication == null)
            {
                return NotFound();
            }

            var publication = await _context.Publication.FindAsync(id);
            if (publication == null)
            {
                return NotFound();
            }
            return View(publication);
        }

        // POST: Publications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Image,Body")] Publication publication)
        {
            if (id != publication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicationExists(publication.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(publication);
        }

        // GET: Publications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Publication == null)
            {
                return NotFound();
            }

            var publication = await _context.Publication
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publication == null)
            {
                return NotFound();
            }

            return View(publication);
        }

        // POST: Publications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Publication == null)
            {
                return Problem("Entity set 'WeblogContext.Publication'  is null.");
            }
            var publication = await _context.Publication.FindAsync(id);
            if (publication != null)
            {
                _context.Publication.Remove(publication);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicationExists(int id)
        {
          return (_context.Publication?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        // Paginación Carga bajo demanda
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 5; // Cantidad de elementos por página
            int pageNumber = (page ?? 1);
            IActionResult result = await ObtenerDatosPorPagina(pageNumber, pageSize);
            int totalItems = await ObtenerNumeroTotalDeDatos();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Crear un objeto de paginación utilizando los datos obtenidos, el número de página actual y el número total de páginas
            IPagedList<IActionResult> pagedData = new StaticPagedList<IActionResult>(new[] { result }, pageNumber, pageSize, totalPages);

            return View(pagedData);
        }

        private async Task<IActionResult> ObtenerDatosPorPagina(int pageNumber, int pageSize)
        {

            int startIndex = (pageNumber - 1) * pageSize;
            var result = await _context.Publication
                .OrderBy(x => x.Date)  // Ordenar por fecha
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            return View(result);
        }

        private async Task<int> ObtenerNumeroTotalDeDatos()
        {
            int totalItems = await ContarRegistrosEnBaseDeDatos();

            return totalItems;
        }

        private async Task<int> ContarRegistrosEnBaseDeDatos()
        {
            int totalItems = await _context.Publication.CountAsync();
            return totalItems;
        }



    }
}
