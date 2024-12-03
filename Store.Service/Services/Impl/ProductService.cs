using Microsoft.Extensions.Logging;
using NanoidDotNet;
using Store.Api.Models;
using Store.Repository;
using Store.Repository.Repositories.Impl;
using Store.Service.Mappings;
using Store.Service.Models;

namespace Store.Service.Services.Impl;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductService> _logger;
    private const int CodeSize = 10;

    public ProductService(ProductRepository productRepository, ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task<Product> AddProduct(ProductRequestDto productDto)
    {
        var generatedCode = Nanoid.Generate(size: CodeSize);
        var newProduct = new Product
        {
            Code = generatedCode,
            Name = productDto.Name
        };
        try
        {
            var savedEntity = await _productRepository.AddProduct(ProductMappings.ToEntity(newProduct));
            return ProductMappings.ToModel(savedEntity);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while trying to add product with name {Name}", productDto.Name);
            throw;
        }
    }
}