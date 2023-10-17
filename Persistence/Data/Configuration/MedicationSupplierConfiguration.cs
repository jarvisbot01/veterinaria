using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class MedicationSupplierConfiguration : IEntityTypeConfiguration<MedicationSupplier>
    {
        public void Configure(EntityTypeBuilder<MedicationSupplier> builder)
        {
            builder.ToTable("medicationSupplier");

            builder.Property(m => m.Id).IsRequired();

            builder
                .HasOne(m => m.Medication)
                .WithMany(m => m.MedicationSuppliers)
                .HasForeignKey(m => m.MedicationId);

            builder
                .HasOne(m => m.Supplier)
                .WithMany(m => m.MedicationSuppliers)
                .HasForeignKey(m => m.SupplierId);
        }
    }
}
