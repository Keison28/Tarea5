using System.ComponentModel.DataAnnotations;

namespace RentMovies.Web.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        [StringLength(60)]

        public string Title { get; set; }
        [StringLength(60)]

        public string Description { get; set; }

        public string Category { get; set; }
    }
}
