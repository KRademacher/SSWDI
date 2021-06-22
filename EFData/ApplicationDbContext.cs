using Core.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace EFData
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Lodging> Lodgings { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TODO: Add relations
        }
    }
}