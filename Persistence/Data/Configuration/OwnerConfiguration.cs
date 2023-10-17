using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable("owner");

            builder.Property(o => o.Id).IsRequired();

            builder
                .Property(o => o.Name)
                .HasColumnName("name")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(o => o.Email)
                .HasColumnName("email")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(o => o.Phone)
                .HasColumnName("phone")
                .HasColumnType("varchar")
                .HasMaxLength(15)
                .IsRequired();

            builder.HasMany(o => o.Pets).WithOne(p => p.Owner).HasForeignKey(p => p.OwnerId);
        }
    }
}
