using Microsoft.AspNetCore.Mvc;
using Store.Api.Models;
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
        var createdProduct = await _productService.AddProduct(productDto);
    }
}