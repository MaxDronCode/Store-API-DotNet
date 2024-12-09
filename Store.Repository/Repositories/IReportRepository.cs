using Store.Repository.Models;

namespace Store.Repository.Repositories;

public interface IReportRepository
{
    Task<IEnumerable<ProductSalesReport>> GetIncomeReport();
}