using Store.Service.Models;

namespace Store.Service.Services;

public interface ISaleService
{
    public Task<Sale> CreateSale(Sale sale);
}