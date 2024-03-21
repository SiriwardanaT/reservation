using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace test.Models;

public partial class ReservationnContext : DbContext
{
    public ReservationnContext()
    {
    }

    public ReservationnContext(DbContextOptions<ReservationnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EnableUser> EnableUsers { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<ReservationRequest> ReservationRequests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-V63A10T;Database=reservationn;Trusted_Connection=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EnableUser>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("EnableUser");

            entity.Property(e => e.Uid)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.ToTable("Image");

            entity.Property(e => e.ImageId)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Pid)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Url)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.Pid);

            entity.ToTable("Property");

            entity.Property(e => e.Pid)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Facilities)
                .HasMaxLength(500)
                .IsFixedLength();
            entity.Property(e => e.Landownerid)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("landownerid");
            entity.Property(e => e.Location)
                .HasMaxLength(500)
                .IsFixedLength()
                .HasColumnName("location");
            entity.Property(e => e.Reason)
                .HasMaxLength(1000)
                .IsFixedLength()
                .HasColumnName("reason");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<ReservationRequest>(entity =>
        {
            entity.HasKey(e => e.Rid);

            entity.ToTable("ReservationRequest");

            entity.Property(e => e.Rid)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.NoOfRooms).HasColumnName("noOfRooms");
            entity.Property(e => e.NoOfStudents).HasColumnName("noOfStudents");
            entity.Property(e => e.Pid)
                .HasMaxLength(500)
                .IsFixedLength()
                .HasColumnName("pid");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Uid)
                .HasMaxLength(500)
                .IsFixedLength()
                .HasColumnName("uid");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
