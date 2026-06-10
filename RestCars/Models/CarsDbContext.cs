using Microsoft.EntityFrameworkCore;
namespace RestCars.Models
{
    public class CarsDbContext : DbContext
    {
        //constructor that takes DbContextOptions and passes it to the base DbContext constructor to database
        public CarsDbContext(DbContextOptions<CarsDbContext> options) : base(options)
        {
        }
        //DbSet property (built-in) represents a table in the database and allows CRUD operations on that table.
        public DbSet<Car> Cars { get; set; }

    }
}
