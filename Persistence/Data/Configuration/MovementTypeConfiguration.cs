using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class MovementTypeConfiguration : IEntityTypeConfiguration<MovementType>
    {
        public void Configure(EntityTypeBuilder<MovementType> builder)
        {
            builder.ToTable("movementType");

            builder.Property(m => m.Id).IsRequired();

            builder
                .Property(m => m.Description)
                .HasColumnName("description")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasMany(m => m.MedicationMovements)
                .WithOne(mm => mm.MovementType)
                .HasForeignKey(mm => mm.MovementTypeId);
        }
    }
}
