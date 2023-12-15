using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace midterm2.Model;

public partial class SelimContext : DbContext
{
    public SelimContext()
    {
    }

    public SelimContext(DbContextOptions<SelimContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<House> Houses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:syaylalimidterm.database.windows.net,1433;Initial Catalog=selim;Persist Security Info=False;User ID=20070006072@stu.yasar.edu.tr;Password=Rzr53abb;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Authentication=Active Directory Password;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<House>()
        .HasKey(h => h.HouseId);

        modelBuilder.Entity<House>()
        .Property(h => h.HouseId)
        .ValueGeneratedOnAdd();

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951ACD3CC761C9");

            entity.HasIndex(e => new { e.HouseId, e.FromDate, e.ToDate }, "UQ__Bookings__B1B0CC36182D235A").IsUnique();

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.FromDate).HasColumnType("date");
            entity.Property(e => e.HouseId).HasColumnName("HouseID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("('Booked')");
            entity.Property(e => e.ToDate).HasColumnType("date");

            entity.HasOne(d => d.Client).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__Bookings__Client__0A9D95DB");

            entity.HasOne(d => d.House).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Bookings__HouseI__0B91BA14");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__E67E1A04E504D1FC");

            entity.HasIndex(e => e.Username, "UQ__Clients__536C85E40688C48C").IsUnique();

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ClientPassword).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<House>(entity =>
        {
            entity.HasKey(e => e.HouseId).HasName("PK__Houses__085D12AFBE89F527");

            entity.Property(e => e.HouseId).HasColumnName("HouseID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
