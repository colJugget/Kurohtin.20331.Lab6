using Microsoft.EntityFrameworkCore;
using Kurohtin.Domain.Entities;

namespace Kurohtin.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Crepezh> Crepezhy { get; set; }
        public DbSet<Category> Categories { get; set; }
    }

}
