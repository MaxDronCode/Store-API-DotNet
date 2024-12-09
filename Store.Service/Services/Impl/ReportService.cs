using Store.Repository.Repositories;
using Store.Service.Models;

namespace Store.Service.Services.Impl;

public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;

    public ReportService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<IEnumerable<ProductSalesInfo>> GetIncomeReport()
    {
        var data = await _reportRepository.GetIncomeReport();

        return data.Select(d => new ProductSalesInfo
        {
            ProductName = d.ProductName,
            ProductCode = d.ProductCode,
            TotalSales = d.TotalSold
        });
    }
}