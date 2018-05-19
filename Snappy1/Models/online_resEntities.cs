using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Snappy1.Models
{
    public partial class online_resEntities : DbContext
    {
        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<CarRental> CarRental { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Location> Location { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;Database=E:\SAD Project\online_rental_data\online_res.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("car");

                entity.HasIndex(e => e.LocationName);

                entity.HasIndex(e => e.ManagerUsername)
                    .HasName("IX_car_CustomerUsername");

                entity.Property(e => e.CarId)
                    .HasColumnName("car_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasColumnName("brand")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryType)
                    .IsRequired()
                    .HasColumnName("category_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasColumnName("color")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocationName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ManagerUsername)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasColumnName("model ")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductionYear).HasColumnName("production_year");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CategoryTypeNavigation)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.CategoryType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_car_category");

                entity.HasOne(d => d.LocationNameNavigation)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.LocationName);

                entity.HasOne(d => d.ManagerUsernameNavigation)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.ManagerUsername)
                    .HasConstraintName("FK_car_customer_CustomerUsername");
            });

            modelBuilder.Entity<CarRental>(entity =>
            {
                entity.HasKey(e => e.RentalId);

                entity.ToTable("car_rental");

                entity.Property(e => e.RentalId).HasColumnName("rental_id");

                entity.Property(e => e.BookingEnddate)
                    .HasColumnName("booking_enddate")
                    .HasColumnType("date");

                entity.Property(e => e.BookingStartdate)
                    .HasColumnName("booking_startdate")
                    .HasColumnType("date");

                entity.Property(e => e.CarId).HasColumnName("car_id");

                entity.Property(e => e.Dropoff)
                    .IsRequired()
                    .HasColumnName("dropoff")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasColumnName("location_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pickup)
                    .IsRequired()
                    .HasColumnName("pickup")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.CarRental)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_car_rental_car");

                entity.HasOne(d => d.LocationNameNavigation)
                    .WithMany(p => p.CarRental)
                    .HasForeignKey(d => d.LocationName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_car_rental_location");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.CarRental)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_car_rental_customer");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryType);

                entity.ToTable("category");

                entity.Property(e => e.CategoryType)
                    .HasColumnName("category_type")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(18, 8)");

                entity.Property(e => e.ImageLoc)
                    .IsRequired()
                    .HasColumnName("image_loc")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("customer");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Dob)
                    .HasColumnName("dob")
                    .HasColumnType("date");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.License)
                    .IsRequired()
                    .HasColumnName("license")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.LocationName);

                entity.ToTable("location");

                entity.Property(e => e.LocationName)
                    .HasColumnName("location_name")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ManagerUsername).HasColumnName("Manager_username");
            });
        }
    }
}
