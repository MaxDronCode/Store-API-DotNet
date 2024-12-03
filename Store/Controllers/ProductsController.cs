using System.Text.RegularExpressions;
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
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
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

    [HttpGet("{code}")]
    public async Task<IActionResult> GetProduct(string code)
    {
        _logger.LogInformation("Request for obtaining product with code {Code}", code);

        if (!isValidCode(code))
        {
            _logger.LogWarning("Invalid code recieved: {Code}.", code);
            return BadRequest("Invalid code format.");
        }

        Product product;
        try
        {
            product = await _productService.GetProductByCode(code);
        }
        catch (ProductNotFoundException e)
        {
            return NotFound(e.Message);
        }
        var responseDto = ProductMappings.ToResponseDto(product);
        return Ok(responseDto);
    }

    private bool isValidCode(string code)
    {
        return Regex.IsMatch(code, @"^[a-zA-Z0-9]{10}$");
    }
}