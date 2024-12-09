using Microsoft.EntityFrameworkCore;
using Store.Repository.Models;

namespace Store.Repository.DbConfig;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ClientEntity> Clients { get; set; }

    public DbSet<ProductEntity> Products { get; set; }

    public DbSet<SaleEntity> Sales { get; set; }

    public DbSet<SaleDetailEntity> SaleDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurar la clave compuesta para SaleDetailEntity
        modelBuilder.Entity<SaleDetailEntity>()
            .HasKey(sd => new { sd.SaleId, sd.ProductCode });

        // Configurar la relación uno a muchos entre Sale y SaleDetails
        modelBuilder.Entity<SaleEntity>()
            .HasMany(s => s.SaleDetails)
            .WithOne(sd => sd.Sale)
            .HasForeignKey(sd => sd.SaleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configurar la relación muchos a uno entre SaleDetail y Product
        modelBuilder.Entity<SaleDetailEntity>()
            .HasOne(sd => sd.Sale)
            .WithMany(s => s.SaleDetails)
            .HasForeignKey(sd => sd.SaleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SaleDetailEntity>()
            .HasOne(sd => sd.Product)
            .WithMany(p => p.SaleDetails)
            .HasForeignKey(sd => sd.ProductCode);

        // Configurar la relación uno a muchos entre Client y Sales
        modelBuilder.Entity<ClientEntity>()
            .HasMany(c => c.Sales)
            .WithOne(s => s.Client)
            .HasForeignKey(s => s.ClientNif);
    }
}