using Store.Api.Models;
using Store.Service.Models;

namespace Store.Service.Services;

public interface IProductService
{
    Task<Product> AddProduct(ProductRequestDto product);

    Task<Product> GetProductByCode(string code);

    Task<IEnumerable<Product>> GetProducts();

    Task<Product> UpdateProduct(Product product);
}