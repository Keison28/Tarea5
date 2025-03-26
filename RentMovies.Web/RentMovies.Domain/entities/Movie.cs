using System.ComponentModel.DataAnnotations;

namespace RentMovies.Domain.Entities
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [StringLength(60)]

        public string Title { get; set; }
        [StringLength(60)]

        public string Description { get; set; }

        public string Category { get; set; }
    }
}
