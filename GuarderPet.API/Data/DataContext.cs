using GuarderPet.API.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GuarderPet.API.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetService> PetServices { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<PetPhoto> PetPhotos { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<PetServiceHistory> PetServiceHistories { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<PlacePhoto> placePhotos { get; set; }
        public DbSet<CareDescription> CareDescriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Breed>().HasIndex(x => x.BreedTittle).IsUnique();
            modelBuilder.Entity<DocumentType>().HasIndex(x => x.Type).IsUnique();
            modelBuilder.Entity<PetService>().HasIndex(x => x.ServiceDetail).IsUnique();
            modelBuilder.Entity<PetType>().HasIndex(x => x.Type).IsUnique();
        }

    }
}
