using NanoidDotNet;
using Store.Api.Models;
using Store.Service.Models;

namespace Store.Service.Services.Impl;

public class ProductService : IProductService
{
    private readonly IProductRespository _productRespository;

    public ProductService(IProductRespository productRespository)
    {
        _productRespository = productRespository;
    }

    private const int CodeSize = 10;

    public Task<Product> AddProduct(ProductRequestDto productDto)
    {
        var generatedCode = Nanoid.Generate(size: CodeSize);
        var newProduct = new Product
        {
            Code = generatedCode,
            Name = productDto.Name
        };
    }
}