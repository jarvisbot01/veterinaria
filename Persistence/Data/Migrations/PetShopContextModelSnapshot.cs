﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Data.Migrations
{
    [DbContext(typeof(PetShopContext))]
    partial class PetShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Entities.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime")
                        .HasColumnName("appointmentDate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar")
                        .HasColumnName("description");

                    b.Property<int>("PetId")
                        .HasColumnType("int");

                    b.Property<int>("VetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.HasIndex("VetId");

                    b.ToTable("appointment", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Breed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.Property<int>("SpeciesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpeciesId");

                    b.ToTable("breed", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Laboratory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar")
                        .HasColumnName("address");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("phone");

                    b.HasKey("Id");

                    b.ToTable("laboratory", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.MedicalTreatment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AdministrationDate")
                        .HasColumnType("datetime")
                        .HasColumnName("administrationDate");

                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<string>("Dose")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("dose");

                    b.Property<int>("MedicationId")
                        .HasColumnType("int");

                    b.Property<string>("Observation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar")
                        .HasColumnName("observation");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("MedicationId");

                    b.ToTable("medicalTreatment", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Medication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("LaboratoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("price");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("Id");

                    b.HasIndex("LaboratoryId");

                    b.ToTable("medication", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.MedicationMovement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MedicationId")
                        .HasColumnType("int");

                    b.Property<int>("MovementTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("Id");

                    b.HasIndex("MedicationId");

                    b.HasIndex("MovementTypeId");

                    b.ToTable("medicationMovement", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.MedicationSupplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MedicationId")
                        .HasColumnType("int");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicationId");

                    b.HasIndex("SupplierId");

                    b.ToTable("medicationSupplier", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.MovementDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MedicationId")
                        .HasColumnType("int");

                    b.Property<int>("MedicationMovementId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("price");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("Id");

                    b.HasIndex("MedicationId");

                    b.HasIndex("MedicationMovementId");

                    b.ToTable("movementDetail", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.MovementType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("description");

                    b.HasKey("Id");

                    b.ToTable("movementType", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar")
                        .HasColumnName("phone");

                    b.HasKey("Id");

                    b.ToTable("owner", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateOnly>("Birthdate")
                        .HasColumnType("date")
                        .HasColumnName("birthdate");

                    b.Property<int>("BreedId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("SpeciesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BreedId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("SpeciesId");

                    b.ToTable("pet", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("refreshToken", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("rolName");

                    b.HasKey("Id");

                    b.ToTable("rol", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Species", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("species", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("address");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar")
                        .HasColumnName("phone");

                    b.HasKey("Id");

                    b.ToTable("supplier", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("userName");

                    b.HasKey("Id");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.UserRol", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RolId");

                    b.HasIndex("RolId");

                    b.ToTable("userRol", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Vet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar")
                        .HasColumnName("phone");

                    b.Property<string>("Specialty")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("specialty");

                    b.HasKey("Id");

                    b.ToTable("vet", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Appointment", b =>
                {
                    b.HasOne("Domain.Entities.Pet", "Pet")
                        .WithMany("Appointments")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Vet", "Vet")
                        .WithMany("Appointments")
                        .HasForeignKey("VetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");

                    b.Navigation("Vet");
                });

            modelBuilder.Entity("Domain.Entities.Breed", b =>
                {
                    b.HasOne("Domain.Entities.Species", "Species")
                        .WithMany("Breeds")
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Species");
                });

            modelBuilder.Entity("Domain.Entities.MedicalTreatment", b =>
                {
                    b.HasOne("Domain.Entities.Appointment", "Appointment")
                        .WithMany("MedicalTreatments")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Medication", "Medication")
                        .WithMany("MedicalTreatments")
                        .HasForeignKey("MedicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Medication");
                });

            modelBuilder.Entity("Domain.Entities.Medication", b =>
                {
                    b.HasOne("Domain.Entities.Laboratory", "Laboratory")
                        .WithMany("Medications")
                        .HasForeignKey("LaboratoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Laboratory");
                });

            modelBuilder.Entity("Domain.Entities.MedicationMovement", b =>
                {
                    b.HasOne("Domain.Entities.Medication", "Medication")
                        .WithMany()
                        .HasForeignKey("MedicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.MovementType", "MovementType")
                        .WithMany("MedicationMovements")
                        .HasForeignKey("MovementTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medication");

                    b.Navigation("MovementType");
                });

            modelBuilder.Entity("Domain.Entities.MedicationSupplier", b =>
                {
                    b.HasOne("Domain.Entities.Medication", "Medication")
                        .WithMany("MedicationSuppliers")
                        .HasForeignKey("MedicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Supplier", "Supplier")
                        .WithMany("MedicationSuppliers")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medication");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Domain.Entities.MovementDetail", b =>
                {
                    b.HasOne("Domain.Entities.Medication", "Medication")
                        .WithMany("MovementDetails")
                        .HasForeignKey("MedicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.MedicationMovement", "MedicationMovement")
                        .WithMany("MovementDetails")
                        .HasForeignKey("MedicationMovementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medication");

                    b.Navigation("MedicationMovement");
                });

            modelBuilder.Entity("Domain.Entities.Pet", b =>
                {
                    b.HasOne("Domain.Entities.Breed", "Breed")
                        .WithMany("Pets")
                        .HasForeignKey("BreedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Owner", "Owner")
                        .WithMany("Pets")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Species", "Species")
                        .WithMany("Pets")
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Breed");

                    b.Navigation("Owner");

                    b.Navigation("Species");
                });

            modelBuilder.Entity("Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.UserRol", b =>
                {
                    b.HasOne("Domain.Entities.Rol", "Rol")
                        .WithMany("UsersRols")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("UsersRols")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Appointment", b =>
                {
                    b.Navigation("MedicalTreatments");
                });

            modelBuilder.Entity("Domain.Entities.Breed", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Domain.Entities.Laboratory", b =>
                {
                    b.Navigation("Medications");
                });

            modelBuilder.Entity("Domain.Entities.Medication", b =>
                {
                    b.Navigation("MedicalTreatments");

                    b.Navigation("MedicationSuppliers");

                    b.Navigation("MovementDetails");
                });

            modelBuilder.Entity("Domain.Entities.MedicationMovement", b =>
                {
                    b.Navigation("MovementDetails");
                });

            modelBuilder.Entity("Domain.Entities.MovementType", b =>
                {
                    b.Navigation("MedicationMovements");
                });

            modelBuilder.Entity("Domain.Entities.Owner", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Domain.Entities.Pet", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("Domain.Entities.Rol", b =>
                {
                    b.Navigation("UsersRols");
                });

            modelBuilder.Entity("Domain.Entities.Species", b =>
                {
                    b.Navigation("Breeds");

                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Domain.Entities.Supplier", b =>
                {
                    b.Navigation("MedicationSuppliers");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("RefreshTokens");

                    b.Navigation("UsersRols");
                });

            modelBuilder.Entity("Domain.Entities.Vet", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}
