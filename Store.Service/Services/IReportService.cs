using Store.Service.Models;

namespace Store.Service.Services;

public interface IReportService
{
    Task<IEnumerable<ProductSalesInfo>> GetIncomeReport();
}