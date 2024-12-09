using Microsoft.EntityFrameworkCore;
using Store.Repository.DbConfig;
using Store.Repository.Models;

namespace Store.Repository.Repositories.Impl;

public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _context;

    public ReportRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductSalesReport>> GetIncomeReport()
    {
        var query = _context.SaleDetails
            .GroupBy(sd => sd.ProductCode)
            .Select(g => new
            {
                ProductCode = g.Key,
                TotalSold = g.Sum(sd => sd.Quantity),
            });

        var result = await query.ToListAsync();

        var detailedResult = from r in result
                             join p in _context.Products on r.ProductCode equals p.Code
                             select new ProductSalesReport
                             {
                                 ProductCode = p.Code,
                                 ProductName = p.Name,
                                 TotalSold = r.TotalSold
                             };
        return detailedResult.OrderByDescending(x => x.TotalSold).ToList();
    }
}