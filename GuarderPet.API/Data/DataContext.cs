using GuarderPet.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuarderPet.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { 
        
        }
        public DbSet<Breed> Breeds { get; set; }
        //public DbSet<CareDescription> CareDescriptions { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        //public DbSet<Pet> Pets { get; set; }
        public DbSet<PetService> PetServices { get; set; }
        //public DbSet<PetServiceHistory> petServiceHistories { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        //public DbSet<PhotoPet> PhotoPets { get; set; }
        //public DbSet<PhotoPlace> PhotoPlaces { get; set; }
        //public DbSet<Place> Places { get; set; }

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
