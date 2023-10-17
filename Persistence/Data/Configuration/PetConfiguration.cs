using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pet");

            builder.Property(p => p.Id).IsRequired();

            builder
                .Property(p => p.Name)
                .HasColumnName("name")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(p => p.Birthdate)
                .HasColumnName("birthdate")
                .HasColumnType("date")
                .IsRequired();

            builder.HasOne(p => p.Breed).WithMany(b => b.Pets).HasForeignKey(p => p.BreedId);

            builder.HasOne(p => p.Species).WithMany(s => s.Pets).HasForeignKey(p => p.SpeciesId);

            builder.HasOne(p => p.Owner).WithMany(o => o.Pets).HasForeignKey(p => p.OwnerId);

            builder.HasMany(p => p.Appointments).WithOne(a => a.Pet).HasForeignKey(a => a.PetId);
        }
    }
}
