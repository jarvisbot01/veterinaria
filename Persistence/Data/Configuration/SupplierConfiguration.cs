using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("supplier");

            builder.Property(s => s.Id).IsRequired();

            builder
                .Property(s => s.Name)
                .HasColumnName("name")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(s => s.Address)
                .HasColumnName("address")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(s => s.Phone)
                .HasColumnName("phone")
                .HasColumnType("varchar")
                .HasMaxLength(15)
                .IsRequired();

            builder
                .HasMany(s => s.MedicationSuppliers)
                .WithOne(ms => ms.Supplier)
                .HasForeignKey(ms => ms.SupplierId);
        }
    }
}
