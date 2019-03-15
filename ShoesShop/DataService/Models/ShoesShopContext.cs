using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataService.Models
{
    public partial class ShoesShopContext : DbContext
    {
        public ShoesShopContext()
        {
        }

        public ShoesShopContext(DbContextOptions<ShoesShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Promotion> Promotion { get; set; }
        public virtual DbSet<Shoes> Shoes { get; set; }
        public virtual DbSet<ShoesHasSize> ShoesHasSize { get; set; }
        public virtual DbSet<Size> Size { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-UH7HU37\\TIENTPSQL;Database=ShoesShop;User ID=sa;Password=1234;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.Point).HasColumnName("point");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.ContactPhone).HasMaxLength(12);

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.CusName).HasMaxLength(100);

                entity.Property(e => e.State).HasMaxLength(10);

                entity.HasOne(d => d.DiscountCodeNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.DiscountCode)
                    .HasConstraintName("FK_Order_Promotion");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetail_Order");

                entity.HasOne(d => d.Shoes)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.ShoesId)
                    .HasConstraintName("FK_OrderDetail_Shoes");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Color).HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Type).HasMaxLength(10);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Product_Brand");
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.Property(e => e.DiscountCode).HasMaxLength(10);

                entity.Property(e => e.Event).HasMaxLength(10);
            });

            modelBuilder.Entity<Shoes>(entity =>
            {
                entity.Property(e => e.Avatar2).HasMaxLength(200);

                entity.Property(e => e.Avatar1).HasMaxLength(200);

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sex).HasDefaultValueSql("((2))");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Shoes)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shoes_Brand");
            });

            modelBuilder.Entity<ShoesHasSize>(entity =>
            {
                entity.HasOne(d => d.Shoes)
                    .WithMany(p => p.ShoesHasSize)
                    .HasForeignKey(d => d.ShoesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoesHasSize_Shoes");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.ShoesHasSize)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoesHasSize_Size");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.Property(e => e.Size1).HasColumnName("Size");
            });
        }
    }
}
