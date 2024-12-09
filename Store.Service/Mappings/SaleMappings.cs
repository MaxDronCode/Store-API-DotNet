using Store.Repository.Models;
using Store.Service.Models;

namespace Store.Service.Mappings;

public static class SaleMappings
{
    public static SaleEntity ToEntity(Sale sale)
    {
        return new SaleEntity
        {
            ClientNif = sale.ClientNif,
            SaleDetails = sale.Items.Select(i => new SaleDetailEntity
            {
                Quantity = i.Quantity,
                ProductCode = i.ProductCode
            }).ToList()
        };
    }

    public static Sale ToModel(SaleEntity saleEntity)
    {
        return new Sale
        {
            Id = saleEntity.Id,
            ClientNif = saleEntity.ClientNif,
            Items = saleEntity.SaleDetails.Select(i => new SaleDetail
            {
                ProductCode = i.ProductCode,
                Quantity = i.Quantity
            }).ToList()
        };
    }
}