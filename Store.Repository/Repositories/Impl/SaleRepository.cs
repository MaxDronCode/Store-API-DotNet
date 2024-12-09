using Microsoft.Extensions.Logging;
using Store.Repository.DbConfig;
using Store.Repository.Models;

namespace Store.Repository.Repositories.Impl;

public class SaleRepository : ISaleRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<ProductRepository> _logger;

    public SaleRepository(AppDbContext context, ILogger<ProductRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SaleEntity> CreateSale(SaleEntity sale)
    {
        try
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Sale created successfully");
            return sale;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while trying to create sale");
            throw;
        }
    }
}