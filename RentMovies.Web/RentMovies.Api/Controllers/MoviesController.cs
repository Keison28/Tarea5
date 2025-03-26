using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentMovies.Domain.Entities;
using RentMovies.Ifraestructure;

namespace RentMovies.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly DataContext _context;

        public MoviesController(DataContext context)
        {
            _context = context;
        }



        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string filter = "")
        {
            var list = await _context.Movies
                .ToListAsync();

            if (!string.IsNullOrEmpty(filter))
            {
                list = list.Where(d => d.Title.ToLower().Contains(filter.ToLower()))
                           .ToList();
            }
            var listToReturn = new List<MovieDto>();
            foreach (var item in list)
            {
                listToReturn.Add(new MovieDto { Title = item.Title, Id = item.Id, Description = item.Description, Category = item.Category });
            }
            return Ok(listToReturn);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var movies = await _context.Movies.FindAsync(id);
            if (movies == null)
            {
                return BadRequest("Not found");
            }
            return Ok(movies);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> Create([FromBody] MovieDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not found");
            }
            var movies = new Movie { Title = dto.Title, Category = dto.Category, Description = dto.Description };
            _context.Movies.Add(movies);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Created successfully!" });
        }

        [HttpPut(nameof(Update))]
        public async Task<IActionResult> Update([FromBody] MovieDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not found");
            }
            var MoviesDB = await _context.Movies.FindAsync(dto.Id);
            if (MoviesDB == null)
            {
                return NotFound($"Movies {dto.Title} was not found");
            }
            MoviesDB.Title = dto.Title;
            MoviesDB.Description = dto.Description;
            MoviesDB.Category = dto.Category;

            _context.Movies.Update(MoviesDB);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Updated successfully!" });
        }


    }
}
