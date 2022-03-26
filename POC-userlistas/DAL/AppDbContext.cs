namespace PGdemoApp.data
{
    using Microsoft.EntityFrameworkCore;
    using PGdemoApp.Core;

    public class AppDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
      

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
