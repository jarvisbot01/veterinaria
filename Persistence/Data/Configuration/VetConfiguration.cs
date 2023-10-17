using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class VetConfiguration : IEntityTypeConfiguration<Vet>
    {
        public void Configure(EntityTypeBuilder<Vet> builder)
        {
            builder.ToTable("vet");

            builder.Property(v => v.Id).IsRequired();

            builder
                .Property(v => v.Name)
                .HasColumnName("name")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(v => v.Email)
                .HasColumnName("email")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(v => v.Phone)
                .HasColumnName("phone")
                .HasColumnType("varchar")
                .HasMaxLength(15)
                .IsRequired();

            builder
                .Property(v => v.Specialty)
                .HasColumnName("specialty")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasMany(v => v.Appointments).WithOne(a => a.Vet).HasForeignKey(a => a.VetId);
        }
    }
}
