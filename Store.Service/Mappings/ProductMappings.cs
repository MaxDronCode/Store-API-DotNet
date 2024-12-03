using Store.Repository.Models;
using Store.Service.Models;

namespace Store.Service.Mappings;

public static class ProductMappings
{
    public static ProductEntity ToEntity(Product product)
    {
        return new ProductEntity
        {
            Code = product.Code,
            Name = product.Name
        };
    }

    public static Product ToModel(ProductEntity entity)
    {
        return new Product
        {
            Code = entity.Code,
            Name = entity.Name
        };
    }
}