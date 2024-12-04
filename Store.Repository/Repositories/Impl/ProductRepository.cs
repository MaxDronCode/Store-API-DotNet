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
        catch (DataAccessException e)
        {
            _logger.LogError(e, "Error while trying to get product by code {Code}", code);
            throw new DataAccessException("Error while trying to get product by code", e);
        }
    }

    public async Task<ProductEntity?> GetProductByName(string name)
    {
        try
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        }
        catch (DataAccessException e)
        {
            _logger.LogError(e, "Error while trying to get product by name {Name}", name);
            throw new DataAccessException("Error while trying to get product by name", e);
        }
    }

    public Task<IEnumerable<ProductEntity>> GetProducts()
    {
        var products = _context.Products;
        return Task.FromResult(products.AsEnumerable());
    }

    public async Task<ProductEntity> UpdateProduct(ProductEntity entity)
    {
        try
        {
            var existingEntity = await _context.Products.FindAsync(entity.Code);
            if (existingEntity == null)
            {
                throw new DataAccessException("Product not found");
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Product with code {Code} updated.", entity.Code);
            return entity;
        }
        catch (DbUpdateConcurrencyException e)
        {
            _logger.LogError(e, "Error while trying to update product with code {Code}", entity.Code);
            throw new DataAccessException("Error while trying to update product", e);
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e, "Error while trying to update product with code {Code}", entity.Code);
            throw new DataAccessException("Error while trying to update product", e);
        }
    }

    public Task DeleteProduct(ProductEntity entity)
    {
        try
        {
            _context.Products.Remove(entity);
            return _context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e, "Error while trying to remove product with code {Code}", entity.Code);
            throw new DataAccessException("Error while trying to remove product", e);
        }
    }
}