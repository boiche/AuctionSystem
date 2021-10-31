﻿using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AuctionSystem.Data.Model
{
    public partial class AuctionSystemContext : DbContext
    { 
        public AuctionSystemContext(DbContextOptions<AuctionSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<AuctionState> AuctionStates { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Auction>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.PublishedOn).HasColumnType("datetime");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Auctions_AuctionStates");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Auctions_Users");
            });

            modelBuilder.Entity<AuctionState>(entity =>
            {
                entity.ToTable("AuctionStates", "noms");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.AuctionId).HasColumnName("AuctionID");

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Auction)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.AuctionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Auctions");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Users");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles", "noms");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RolesList).HasMaxLength(200);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(125);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .HasMaxLength(150)
                    .HasComputedColumnSql("(([FirstName]+[SecondName])+[ThirdName])", false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.SecondName).HasMaxLength(50);

                entity.Property(e => e.ThirdName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Role)
                    .HasConstraintName("FK_Users_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
