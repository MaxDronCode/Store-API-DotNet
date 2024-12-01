using Microsoft.EntityFrameworkCore;
using Store.Repository.Models;

namespace Store.Repository.DbConfig;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ClientEntity> Clients { get; set; }
}