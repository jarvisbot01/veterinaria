using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class MedicalTreatmentConfiguration : IEntityTypeConfiguration<MedicalTreatment>
    {
        public void Configure(EntityTypeBuilder<MedicalTreatment> builder)
        {
            builder.ToTable("medicalTreatment");

            builder.Property(p => p.Id).IsRequired();

            builder
                .Property(p => p.Dose)
                .HasColumnName("dose")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(p => p.AdministrationDate)
                .HasColumnName("administrationDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(p => p.Observation)
                .HasColumnName("observation")
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
