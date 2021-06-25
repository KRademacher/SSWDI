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
        public DbSet<InterestedAnimal> InterestedAnimals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Make email unique
            modelBuilder.Entity<Volunteer>()
                .HasIndex(v => v.EmailAddress)
                .IsUnique();
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.EmailAddress)
                .IsUnique();

            // Many to many relationship between Customer and Animal.
            // A customer can show interest in multiple animals.
            // An animal can have multiple customers interested.
            modelBuilder.Entity<InterestedAnimal>()
                .HasKey(ia => new { ia.AnimalID, ia.CustomerID });
            modelBuilder.Entity<InterestedAnimal>()
                .HasOne(ia => ia.Animal)
                .WithMany(a => a.InterestedAdoptees)
                .HasForeignKey(ia => ia.AnimalID);
            modelBuilder.Entity<InterestedAnimal>()
                .HasOne(ia => ia.Customer)
                .WithMany(c => c.AnimalsInterestedIn)
                .HasForeignKey(ia => ia.CustomerID);

            modelBuilder.Entity<Animal>(animal =>
            {
                // Each animal can have multiple comments.
                animal.OwnsMany(a => a.Comments)
                    .HasOne(c => c.CommentedOn);
                // Each animal can have multiple treatments.
                animal.OwnsMany(a => a.Treatments)
                    .HasOne(t => t.PerformedOn);
                // A customer can adopt multiple animals, but an animal can only be adopted once.
                animal.HasOne(a => a.AdoptedBy)
                    .WithMany(c => c.AdoptedAnimals)
                    .HasForeignKey(a => a.AdoptedByID)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // A lodging can house multiple animals
            modelBuilder.Entity<Lodging>()
                .HasMany(l => l.LodgingAnimals)
                .WithOne(a => a.LodgingLocation)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}