using Microsoft.EntityFrameworkCore;

namespace Application.Models
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<BinaryData> Images { get; set; }

        public virtual DbSet<AdminLogin> AdminLogins { get; set; }

        public ApplicationDbContext() : base(
            new DbContextOptionsBuilder<DbContext>()
            .UseSqlServer(Startup.CONNECTION_STRING).Options) {


        }

        public ApplicationDbContext(DbContextOptions options) : base(options) {
 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}