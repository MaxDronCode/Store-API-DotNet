using Store.Repository.Models;

namespace Store.Repository.Repositories;

public interface IMongoSaleLogRepository
{
    Task AddSaleLog(SaleLogEntity entity);

    Task<IEnumerable<SaleLogEntity>> GetSaleLogsByClientNif(string clientNif);
}