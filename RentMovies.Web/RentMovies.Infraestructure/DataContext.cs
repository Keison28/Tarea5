using Microsoft.EntityFrameworkCore;

namespace RentMovies.Ifraestructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<RentMovies.Domain.Entities.Movie> Movies { get; set; } = default!;
        public DbSet<RentMovies.Domain.Entities.Category> Categories { get; set; } = default!;

    }
}
