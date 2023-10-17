using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class PetShopContext : DbContext
    {
        public PetShopContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Laboratory> Laboratories { get; set; }
        public DbSet<MedicalTreatment> MedicalTreatments { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicationMovement> MedicationMovements { get; set; }
        public DbSet<MedicationSupplier> MedicationSuppliers { get; set; }
        public DbSet<MovementDetail> MovementDetails { get; set; }
        public DbSet<MovementType> MovementTypes { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRol> UsersRols { get; set; }
        public DbSet<Vet> Vets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
