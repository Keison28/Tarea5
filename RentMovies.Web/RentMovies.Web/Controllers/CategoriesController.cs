using Microsoft.AspNetCore.Mvc;
using RentMovies.Ifraestructure;
using RentMovies.Web.Models;

namespace RentMovies.Web
{
    public class CategoriesController : Controller
    {
        private readonly DataContext _context;

        public CategoriesController()
        {
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            return View(new CategoryViewModel { Id = id });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        public async Task<IActionResult> Edit(int id)
        {
            return View(new CategoryViewModel { Id = id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(new CategoryViewModel { Id = id });
        }

    }
}

