using Microsoft.Extensions.Logging;
using NanoidDotNet;
using Store.Api.Models;
using Store.Exceptions;
using Store.Repository;
using Store.Repository.Exceptions;
using Store.Repository.Models;
using Store.Service.Mappings;
using Store.Service.Models;

namespace Store.Service.Services.Impl;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductService> _logger;
    private const int CodeSize = 10;

    public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task<Product> AddProduct(ProductRequestDto productDto)
    {
        var entityOrNull = await _productRepository.GetProductByName(productDto.Name);
        if (entityOrNull != null)
        {
            _logger.LogWarning("Product with name {Name} already exists.", productDto.Name);
            throw new ProductAlreadyExistsException($"A product with the name '{productDto.Name}' already exists.");
        }

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

    public async Task<Product> GetProductByCode(string code)
    {
        _logger.LogInformation("Getting product by code {Code}", code);
        ProductEntity? entityOrNull;
        try
        {
            entityOrNull = await _productRepository.GetProductByCode(code);
        }
        catch (DataAccessException e)
        {
            _logger.LogError(e, "Error while trying to get product by code {Code}", code);
            throw;
        }

        if (entityOrNull != null)
        {
            return ProductMappings.ToModel(entityOrNull);
        }
        else
        {
            throw new ProductNotFoundException($"Product with code '{code}' not found.");
        }
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        var products = await _productRepository.GetProducts();
        return products.Select(ProductMappings.ToModel);
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        var existingProductWithCode = await _productRepository.GetProductByCode(product.Code);

        if (existingProductWithCode == null)
        {
            _logger.LogWarning("Product with code {Code} not found.", product.Code);
            throw new ProductNotFoundException($"Product with code '{product.Code}' not found.");
        }

        var existingProductWithName = await _productRepository.GetProductByName(product.Name);

        if (existingProductWithName != null)
        {
            _logger.LogWarning("Product with name {Name} already exists.", product.Name);
            throw new ProductAlreadyExistsException($"A product with the name '{product.Name}' already exists.");
        }

        ProductEntity entity;
        try
        {
            entity = await _productRepository.UpdateProduct(ProductMappings.ToEntity(product));
        }
        catch (DataAccessException e)
        {
            _logger.LogError(e, "Error while trying to update product with code {Code}", product.Code);
            throw;
        }

        return ProductMappings.ToModel(entity);
    }

    public async Task DeleteProduct(string code)
    {
        var existingProduct = await _productRepository.GetProductByCode(code);

        if (existingProduct == null)
        {
            _logger.LogWarning("Product with code {Code} not found.", code);
            throw new ProductNotFoundException($"Product with code '{code}' not found.");
        }

        try
        {
            await _productRepository.DeleteProduct(existingProduct);
        }
        catch (DataAccessException e)
        {
            _logger.LogError(e, "Error while trying to delete product with code {Code}", code);
            throw;
        }
    }
}