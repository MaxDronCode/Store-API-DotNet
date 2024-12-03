using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Repository.DbConfig;
using Store.Repository.Exceptions;
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

    public async Task<ProductEntity?> GetProductByCode(string code)
    {
        try
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Code == code);
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e, "Error while trying to get product by code {Code}", code);
            throw new DataAccessException("Error while trying to get product by code", e);
        }
    }

    public async Task<ProductEntity?> GetProductByName(string name)
    {
        var entityOrNull = await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        return entityOrNull;
    }
}