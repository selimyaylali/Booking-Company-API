﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using midterm2.Model;

#nullable disable

namespace midterm2.Migrations
{
    [DbContext(typeof(SelimContext))]
    [Migration("20231214172656_AdjustHouseId")]
    partial class AdjustHouseId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("midterm2.Model.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BookingID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<int?>("ClientId")
                        .HasColumnType("int")
                        .HasColumnName("ClientID");

                    b.Property<DateTime?>("FromDate")
                        .HasColumnType("date");

                    b.Property<int?>("HouseId")
                        .HasColumnType("int")
                        .HasColumnName("HouseID");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("('Booked')");

                    b.Property<DateTime?>("ToDate")
                        .HasColumnType("date");

                    b.HasKey("BookingId")
                        .HasName("PK__Bookings__73951ACD3CC761C9");

                    b.HasIndex("ClientId");

                    b.HasIndex(new[] { "HouseId", "FromDate", "ToDate" }, "UQ__Bookings__B1B0CC36182D235A")
                        .IsUnique()
                        .HasFilter("[HouseID] IS NOT NULL AND [FromDate] IS NOT NULL AND [ToDate] IS NOT NULL");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("midterm2.Model.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ClientID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientId"));

                    b.Property<string>("ClientPassword")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ClientId")
                        .HasName("PK__Clients__E67E1A04E504D1FC");

                    b.HasIndex(new[] { "Username" }, "UQ__Clients__536C85E40688C48C")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("midterm2.Model.House", b =>
                {
                    b.Property<int>("HouseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("HouseID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HouseId"));

                    b.Property<string>("Amenities")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaxGuests")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("HouseId")
                        .HasName("PK__Houses__085D12AFBE89F527");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("midterm2.Model.Booking", b =>
                {
                    b.HasOne("midterm2.Model.Client", "Client")
                        .WithMany("Bookings")
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK__Bookings__Client__0A9D95DB");

                    b.HasOne("midterm2.Model.House", "House")
                        .WithMany("Bookings")
                        .HasForeignKey("HouseId")
                        .HasConstraintName("FK__Bookings__HouseI__0B91BA14");

                    b.Navigation("Client");

                    b.Navigation("House");
                });

            modelBuilder.Entity("midterm2.Model.Client", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("midterm2.Model.House", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
