using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class MovementDetailConfiguration : IEntityTypeConfiguration<MovementDetail>
    {
        public void Configure(EntityTypeBuilder<MovementDetail> builder)
        {
            builder.ToTable("movementDetail");

            builder.Property(m => m.Id).IsRequired();

            builder
                .Property(m => m.Quantity)
                .HasColumnName("quantity")
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(m => m.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder
                .HasOne(m => m.Medication)
                .WithMany(m => m.MovementDetails)
                .HasForeignKey(m => m.MedicationId);

            builder
                .HasOne(m => m.MedicationMovement)
                .WithMany(m => m.MovementDetails)
                .HasForeignKey(m => m.MedicationMovementId);
        }
    }
}
