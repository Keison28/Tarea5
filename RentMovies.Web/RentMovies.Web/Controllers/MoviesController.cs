using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentMovies.Domain.Entities;
using RentMovies.Ifraestructure;
using RentMovies.Web.Models;

namespace RentMovies.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly DataContext _context;

        public MoviesController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string title, string brand)
        {
            var entities = _context.Movies.Where(p => p.Id > 0);
            var filterResult = entities;
            if (!string.IsNullOrEmpty(title))
            {
                filterResult = entities.Where(v => v.Title.ToLower().Contains(title.ToLower()));
            }

            return View(await filterResult.ToListAsync());
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        public async Task<IActionResult> Create()
        {
            var movie = new MovieViewModel();

            return View(movie);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var entity = new Movie();
                entity.Title = viewModel.Title;
                entity.Description = viewModel.Description;
                entity.Category = viewModel.Category;


                _context.Movies.Add(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _context.Movies.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        private async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExist(movie.Id))
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
            return View(movie);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entity = await _context.Movies.FindAsync(id);
            if (entity != null)
            {
                _context.Movies.Remove(entity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }


        private bool MovieExist(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
