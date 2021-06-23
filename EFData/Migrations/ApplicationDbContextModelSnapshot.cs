﻿// <auto-generated />
using System;
using EFData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFData.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.DomainModel.Animal", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Adoptable")
                        .HasColumnType("bit");

                    b.Property<int?>("AdoptedByID")
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("AnimalType")
                        .HasColumnType("int");

                    b.Property<string>("Breed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfAdoption")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfArrival")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfPassing")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EstimatedAge")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsChildFriendly")
                        .HasColumnType("int");

                    b.Property<bool>("IsNeutered")
                        .HasColumnType("bit");

                    b.Property<string>("LeavingReason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LodgingID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AdoptedByID");

                    b.HasIndex("LodgingID");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("Core.DomainModel.Customer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<string>("HouseNumberAddition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegistrationNumber")
                        .HasColumnType("int");

                    b.Property<string>("StreetName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Core.DomainModel.InterestedAnimal", b =>
                {
                    b.Property<int>("AnimalID")
                        .HasColumnType("int");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.HasKey("AnimalID", "CustomerID");

                    b.HasIndex("CustomerID");

                    b.ToTable("InterestedAnimal");
                });

            modelBuilder.Entity("Core.DomainModel.Lodging", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnimalType")
                        .HasColumnType("int");

                    b.Property<int>("CurrentCapacity")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LodgingType")
                        .HasColumnType("int");

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Lodgings");
                });

            modelBuilder.Entity("Core.DomainModel.Volunteer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.ToTable("Volunteers");
                });

            modelBuilder.Entity("Core.DomainModel.Animal", b =>
                {
                    b.HasOne("Core.DomainModel.Customer", "AdoptedBy")
                        .WithMany("AdoptedAnimals")
                        .HasForeignKey("AdoptedByID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Core.DomainModel.Lodging", "LodgingLocation")
                        .WithMany("LodgingAnimals")
                        .HasForeignKey("LodgingID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsMany("Core.DomainModel.Comment", "Comments", b1 =>
                        {
                            b1.Property<int>("ID")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("AnimalID")
                                .HasColumnType("int");

                            b1.Property<string>("Author")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Content")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("datetime2");

                            b1.HasKey("ID");

                            b1.HasIndex("AnimalID");

                            b1.ToTable("Comments");

                            b1.WithOwner("CommentedOn")
                                .HasForeignKey("AnimalID");
                        });

                    b.OwnsMany("Core.DomainModel.Treatment", "Treatments", b1 =>
                        {
                            b1.Property<int>("ID")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("AnimalID")
                                .HasColumnType("int");

                            b1.Property<double>("Cost")
                                .HasColumnType("float");

                            b1.Property<string>("Description")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("MinimumAge")
                                .HasColumnType("int");

                            b1.Property<DateTime>("PerformDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("PerformedBy")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("TreatmentType")
                                .HasColumnType("int");

                            b1.HasKey("ID");

                            b1.HasIndex("AnimalID");

                            b1.ToTable("Treatments");

                            b1.WithOwner("PerformedOn")
                                .HasForeignKey("AnimalID");
                        });
                });

            modelBuilder.Entity("Core.DomainModel.InterestedAnimal", b =>
                {
                    b.HasOne("Core.DomainModel.Animal", "Animal")
                        .WithMany("InterestedAdoptees")
                        .HasForeignKey("AnimalID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.DomainModel.Customer", "Customer")
                        .WithMany("AnimalsInterestedIn")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
