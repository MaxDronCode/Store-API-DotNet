﻿using Store.Api.Models;
using Store.Service.Models;

namespace Store.Api.Mappings;

public static class ProductMappings
{
    public static Product ToDomainModel(ProductRequestDto dto)
    {
        return new Product
        {
            Code = dto.Code,
            Name = dto.Name
        };
    }

    public static ProductResponseDto ToResponseDto(Product domain)
    {
        return new ProductResponseDto
        {
            Code = domain.Code,
            Name = domain.Name
        };
    }
}