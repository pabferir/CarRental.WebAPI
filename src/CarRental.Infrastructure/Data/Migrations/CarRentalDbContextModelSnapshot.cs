﻿// <auto-generated />
using System;
using CarRental.Infrastructure.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarRental.Infrastructure.Data.Migrations
{
    [DbContext(typeof(CarRentalDbContext))]
    partial class CarRentalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CarRental.Core.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("dateOfBirth");

                    b.Property<string>("IdentityNumber")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("identityNumber");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("surname");

                    b.Property<string>("TelephoneNumber")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("telephoneNumber");

                    b.HasKey("Id");

                    b.ToTable("Customer", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
