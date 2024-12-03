using Store.Repository.Models;

namespace Store.Repository;

public interface IProductRepository
{
    Task<ProductEntity> AddProduct(ProductEntity entity);
}