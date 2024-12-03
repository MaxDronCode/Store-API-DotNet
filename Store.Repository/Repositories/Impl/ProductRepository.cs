using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Repository.DbConfig;
using Store.Repository.Models;

namespace Store.Repository.Repositories.Impl;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(AppDbContext context, ILogger<ProductRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ProductEntity> AddProduct(ProductEntity entity)
    {
        _context.Products.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<ProductEntity?> GetProductByName(string name)
    {
        var entityOrNull = await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        return entityOrNull;
    }
}