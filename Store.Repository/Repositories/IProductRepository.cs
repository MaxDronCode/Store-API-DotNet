﻿using Store.Repository.Models;

namespace Store.Repository;

public interface IProductRepository
{
    Task<ProductEntity> AddProduct(ProductEntity entity);

    Task<ProductEntity?> GetProductByName(string name);

    Task<ProductEntity?> GetProductByCode(string code);
}