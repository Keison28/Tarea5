using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentMovies.Domain.Entities;
using RentMovies.Ifraestructure;

namespace RentMovies.Web
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriesController(DataContext context)
        {
            _context = context;
        }



        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string filter = "")
        {
            var list = await _context.Categories
                .ToListAsync();

            if (!string.IsNullOrEmpty(filter))
            {
                list = list.Where(d => d.Name.ToLower().Contains(filter.ToLower()))
                           .ToList();
            }
            var listToReturn = new List<CategoryDto>();
            foreach (var item in list)
            {
                listToReturn.Add(new CategoryDto { Name = item.Name, Id = item.Id});
            }
            return Ok(listToReturn);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _context.Categories.FindAsync(id);
            if (entity == null)
            {
                return BadRequest("Not found");
            }
            return Ok(entity);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> Create([FromBody] CategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not found");
            }
            var entity = new Category { Name = dto.Name};
            _context.Categories.Add(entity);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Created successfully!" });
        }

        [HttpPut(nameof(Update))]
        public async Task<IActionResult> Update([FromBody] CategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not found");
            }
            var entity = await _context.Categories.FindAsync(dto.Id);
            if (entity == null)
            {
                return NotFound($"Categories {dto.Name} was not found");
            }
            entity.Name = dto.Name;

            _context.Categories.Update(entity);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Updated successfully!" });
        }


    }

}

