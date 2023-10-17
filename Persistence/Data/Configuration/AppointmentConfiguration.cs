using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            {
                builder.ToTable("appointment");

                builder.Property(p => p.Id).IsRequired();

                builder
                    .Property(p => p.AppointmentDate)
                    .HasColumnName("appointmentDate")
                    .HasColumnType("datetime")
                    .IsRequired();

                builder
                    .Property(p => p.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar")
                    .HasMaxLength(255)
                    .IsRequired();

                builder
                    .HasOne(p => p.Pet)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(p => p.PetId);

                builder
                    .HasOne(p => p.Vet)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(p => p.VetId);
            }
        }
    }
}
