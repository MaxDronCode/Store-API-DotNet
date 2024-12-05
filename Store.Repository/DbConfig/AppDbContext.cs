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
}