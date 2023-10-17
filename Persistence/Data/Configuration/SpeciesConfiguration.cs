using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
    {
        public void Configure(EntityTypeBuilder<Species> builder)
        {
            builder.ToTable("species");

            builder.Property(s => s.Id).IsRequired();

            builder
                .Property(s => s.Name)
                .HasColumnName("name")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasMany(s => s.Pets).WithOne(p => p.Species).HasForeignKey(p => p.SpeciesId);

            builder.HasMany(s => s.Breeds).WithOne(b => b.Species).HasForeignKey(b => b.SpeciesId);
        }
    }
}
