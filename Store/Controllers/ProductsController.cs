using Microsoft.AspNetCore.Mvc;
using Store.Api.Mappings;
using Store.Api.Models;
using Store.Exceptions;
using Store.Service.Models;
using Store.Service.Services;

namespace Store.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductRequestDto productDto)
    {
        Product createdProduct;
        try
        {
            createdProduct = await _productService.AddProduct(productDto);
        }
        catch (ProductAlreadyExistsException e)
        {
            return Conflict(e.Message);
        }
        var responseDto = ProductMappings.ToResponseDto(createdProduct);
        return CreatedAtAction(nameof(GetProduct), new { code = responseDto.Code }, responseDto);
    }
}