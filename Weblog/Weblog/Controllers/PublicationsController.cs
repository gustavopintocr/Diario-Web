using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList;
using Microsoft.Extensions.Options;
using Weblog.Data;
using Weblog.Models;
using Weblog.Models.DTOs;

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
            var weblogContext = _context.Publication;
            return View(await weblogContext.ToListAsync());
        }

        private List<Publication> ObtenerUltimasPublicaciones()
        {
            // Lógica para obtener las últimas publicaciones desde el controlador de Publicaciones
            // Puedes utilizar Entity Framework para acceder a la base de datos y obtener los datos
            // Ejemplo:
            return _context.Publication.OrderByDescending(p => p.Date).Take(5).ToList();
            
        }

        // GET: Publications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Publication == null)
            {
                return NotFound();
            }

            var publication = await _context.Publication.Include(x => x.User)
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
            ViewBag.Categories = _context.Category.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Body,UserId")] Publication publication, IFormFile? imageFile, int[] Categories )
        {
            publication.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            publication.Date = DateTime.Now;
            foreach (var item in Categories) {
                var category = await _context.Category
                                           .FirstOrDefaultAsync(m => m.Id == item);
                publication.Categories.Add(category);
            }

            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    byte[] imageBytes = null;

                    using (var memoryStream = new MemoryStream())
                    {
                        imageFile.CopyTo(memoryStream);
                        imageBytes = memoryStream.ToArray();
                    }

                    publication.Image = imageBytes;
                }
                else
                {
                    string defaultImagePath = Path.Combine("wwwroot", "Default.png");
                    byte[] defaultImageBytes = null;

                    using (var fileStream = new FileStream(defaultImagePath, FileMode.Open, FileAccess.Read))
                    using (var memoryStream = new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        defaultImageBytes = memoryStream.ToArray();
                    }

                    publication.Image = defaultImageBytes;
                }
                    await AssignUserRole(publication.UserId);
                    _context.Add(publication);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            return View("Home", "HomeController");
        }

        public async Task<bool> AssignUserRole(string userId)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Name == "Author");
            var userRole = await _context.UserRoles
                            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == role.Id);

            if (userRole == null)
            {
                // If the user is not in the role, add it
                userRole = new IdentityUserRole<string> { UserId = userId, RoleId = role.Id };
                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync();
            }
            return userRole != null;
        }
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
                return RedirectToAction("Index", "Home");
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

            var publication = await _context.Publication.Include(p => p.User).FirstOrDefaultAsync(m => m.Id == id);
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
            return RedirectToAction("Index", "Home");
        }

        private bool PublicationExists(int id)
        {
          return (_context.Publication?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> PublicationsByAuthor(string? authorId)
        {
            if (authorId == null || _context.Publication == null)
            {
                return NotFound();
            }
            PublicationsAuthor publicationsAuthor = new PublicationsAuthor();
            publicationsAuthor.Publications = await _context.Publication.Include(p => p.User).Where(p => p.UserId == authorId).ToListAsync();
            publicationsAuthor.Author = await _context.Users.FirstOrDefaultAsync(m => m.Id == authorId);

            return View(publicationsAuthor);
        }

        public async Task<IActionResult> PublicationsByCategory(int? categoryId)
        {
            if (categoryId == null || _context.Publication == null)
            {
                return NotFound();
            }
            PublicationsCategory publicationsCategory = new PublicationsCategory();
            publicationsCategory.Category = await _context.Category.FirstOrDefaultAsync(m => m.Id == categoryId);
            publicationsCategory.Publications = await _context.Publication.Include(p => p.User).Where(p => p.Categories.Any(c => c.Id == categoryId)).ToListAsync();

            return View(publicationsCategory);
        }
    }
}
