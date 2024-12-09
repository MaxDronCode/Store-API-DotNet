using Store.Api.Models;
using Store.Service.Models;

namespace Store.Api.Mappings;

public static class SaleMappings
{
    public static Sale ToModel(SaleRequestDto saleDto)
    {
        return new Sale
        {
            ClientNif = saleDto.ClientNif,
            Items = saleDto.Items.Select(i => new SaleDetail
            {
                ProductName = i.ProductName,
                Quantity = i.Quantity
            }).ToList()
        };
    }

    public static SaleResponseDto ToDto(Sale sale)
    {
        return new SaleResponseDto
        {
            Id = sale.Id,
            ClientNif = sale.ClientNif,
            Items = sale.Items.Select(i => new SaleItemResponseDto
            {
                ProductName = i.ProductName,
                Quantity = i.Quantity
            }).ToList()
        };
    }
}