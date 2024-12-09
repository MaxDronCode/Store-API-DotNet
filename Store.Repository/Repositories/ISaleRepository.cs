using Store.Repository.Models;

namespace Store.Repository.Repositories;

public interface ISaleRepository
{
    Task<SaleEntity> CreateSale(SaleEntity sale);

    Task<IEnumerable<SaleEntity>> GetSalesByClientNif(string nif);
}