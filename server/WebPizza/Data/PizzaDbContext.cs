using Microsoft.EntityFrameworkCore;
using WebPizza.Data.Entities;

namespace WebPizza.Data
{
    public class PizzaDbContext : DbContext
    {
        public PizzaDbContext(DbContextOptions<PizzaDbContext> options) : base(options) { }

        public DbSet<CategoryEntity> Categories { get; set; }
    }
}
