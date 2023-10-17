using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class MedicationMovementConfiguration : IEntityTypeConfiguration<MedicationMovement>
    {
        public void Configure(EntityTypeBuilder<MedicationMovement> builder)
        {
            builder.ToTable("medicationMovement");

            builder.Property(m => m.Id).IsRequired();

            builder
                .Property(m => m.Quantity)
                .HasColumnName("quantity")
                .HasColumnType("int")
                .IsRequired();

            builder
                .HasOne(m => m.MovementType)
                .WithMany(m => m.MedicationMovements)
                .HasForeignKey(m => m.MovementTypeId);

            builder
                .HasMany(m => m.MovementDetails)
                .WithOne(md => md.MedicationMovement)
                .HasForeignKey(md => md.MedicationMovementId);
        }
    }
}
