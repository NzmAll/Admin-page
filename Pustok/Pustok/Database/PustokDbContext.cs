using Microsoft.EntityFrameworkCore;
using Pustok.Database.Base;
using Pustok.Database.Models;

namespace Pustok.Database;

public class PustokDbContext : DbContext
{
    public PustokDbContext(DbContextOptions<PustokDbContext> options)
        : base(options) { }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is not IAuditable)
                continue;

            IAuditable auditable = (IAuditable)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                auditable.CreatedAt = DateTime.UtcNow;
                auditable.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                auditable.UpdatedAt = DateTime.UtcNow;
            }
        }


        return base.SaveChanges();
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Category product

        //FLUENT API
        modelBuilder
            .Entity<CategoryProduct>()
            .ToTable("CategoryProducts");

        modelBuilder
           .Entity<CategoryProduct>()
           .HasKey(cp => new { cp.ProductId, cp.CategoryId });

        modelBuilder
            .Entity<CategoryProduct>()
            .HasOne<Product>(ct => ct.Product)
            .WithMany(p => p.CategoryProducts)
            .HasForeignKey(ct => ct.ProductId);

        modelBuilder
            .Entity<CategoryProduct>()
            .HasOne<Category>(ct => ct.Category)
            .WithMany(p => p.CategoryProducts)
            .HasForeignKey(ct => ct.CategoryId);


        #endregion

        #region Product colors

        modelBuilder
            .Entity<ProductColor>()
            .HasKey(pc => new { pc.ProductId, pc.ColorId });

        modelBuilder
           .Entity<ProductColor>()
           .HasOne<Product>(pc => pc.Product)
           .WithMany(p => p.ProductColors)
           .HasForeignKey(pc => pc.ProductId);

        modelBuilder
           .Entity<ProductColor>()
           .HasOne<Color>(pc => pc.Color)
           .WithMany(p => p.ProductColors)
           .HasForeignKey(pc => pc.ColorId);

        #endregion

        #region Product size

        modelBuilder
            .Entity<ProductSize>()
            .HasKey(ps => new { ps.ProductId, ps.SizeId });

        modelBuilder
           .Entity<ProductSize>()
           .HasOne<Product>(ps => ps.Product)
           .WithMany(ps => ps.ProductSizes)
           .HasForeignKey(ps => ps.ProductId);

        modelBuilder
          .Entity<ProductSize>()
          .HasOne<Size>(ps => ps.Size)
          .WithMany(s => s.ProductSizes)
          .HasForeignKey(ps => ps.SizeId);

        #endregion

        #region Sizes

        modelBuilder
            .Entity<Size>()
            .ToTable("Size");

        modelBuilder
            .Entity<Size>()
            .HasData(
                new Size
                {
                    Id = -1,
                    Name = "X",
                    UpdatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                },
                new Size
                {
                    Id = -2,
                    Name = "S",
                    UpdatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                },
                new Size
                {
                    Id = -3,
                    Name = "XS",
                    UpdatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                },
                new Size
                {
                    Id = -4,
                    Name = "L",
                    UpdatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                },
                new Size
                {
                    Id = -5,
                    Name = "XL",
                    UpdatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                },
                new Size
                {
                    Id = -6,
                    Name = "XXL",
                    UpdatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                }
            );

        #endregion

        #region Colors

        modelBuilder
           .Entity<Color>()
           .ToTable("Color");

        modelBuilder
            .Entity<Color>()
            .HasData(
                new Color
                {
                    Id = -1,
                    Name = "Blue",
                    UpdatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                },
                new Size
                {
                    Id = -2,
                    Name = "Red",
                    UpdatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                },
                new Size
                {
                    Id = -3,
                    Name = "Green",
                    UpdatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                },
                new Size
                {
                    Id = -4,
                    Name = "Purple",
                    UpdatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                },
                new Size
                {
                    Id = -5,
                    Name = "Yellow",
                    UpdatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                },
                new Size
                {
                    Id = -6,
                    Name = "Black",
                    UpdatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2023, 08, 30, 0, 0, 0, DateTimeKind.Utc),
                }
            );

        #endregion

        base.OnModelCreating(modelBuilder);
    }


    public DbSet<Product> Products { get; set; }
    public DbSet<CategoryProduct> CategoryProducts { get; set; }
    public DbSet<SlideBanner> SlideBanners { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<EmailMessage> EmailMessages { get; set; }
    public DbSet<ProductColor> ProductColors { get; set; }
    public DbSet<ProductSize> ProductSizes { get; set; }

}
