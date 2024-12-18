﻿using Microsoft.AspNetCore.Mvc;
using Store.Api.Mappings;
using Store.Api.Models;
using Store.Exceptions;
using Store.Helpers;
using Store.Repository.Exceptions;
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
        return CreatedAtAction(nameof(GetProductByCode), new { code = responseDto.Code }, responseDto);
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetProductByCode(string code)
    {
        _logger.LogInformation("Request for obtaining product with code {Code}", code);

        if (!Validator.isValidCode(code))
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
            _logger.LogWarning("Product with code {Code} not found.", code);
            return NotFound(e.Message);
        }
        catch (DataAccessException e)
        {
            _logger.LogError(e, "Error while trying to get product with code {Code}", code);
            return StatusCode(500, new { message = "An error occured during the request." });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while trying to get product with code {Code}", code);
            return StatusCode(500, new { message = "An error occured during the request." });
        }

        var responseDto = ProductMappings.ToResponseDto(product);
        return Ok(responseDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetProducts();
        var responseDtos = products.Select(ProductMappings.ToResponseDto);
        return Ok(responseDtos);
    }

    [HttpPut("{code}")]
    public async Task<IActionResult> UpdateProduct(string code, ProductRequestDto productDto)
    {
        _logger.LogInformation("Request for updating product with code {Code}", code);
        if (!Validator.isValidCode(code))
        {
            ModelState.AddModelError("InvalidCode", "Invalid code format.");
            return BadRequest(ModelState);
        }

        var product = ProductMappings.ToDomainModel(code, productDto);
        try
        {
            var updattedProduct = await _productService.UpdateProduct(product);
            return Ok(ProductMappings.ToResponseDto(updattedProduct));
        }
        catch (ProductNotFoundException e)
        {
            _logger.LogWarning("Product with code {Code} not found.", code);
            return NotFound(e.Message);
        }
        catch (ProductAlreadyExistsException e)
        {
            _logger.LogWarning("Product with name {Name} already exists.", product.Name);
            return Conflict(e.Message);
        }
        catch (DataAccessException e)
        {
            _logger.LogError(e, "Error while trying to update product with code {Code}", code);
            return StatusCode(500, new { message = "An error occured during the request." });
        }
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> DeleteProduct(string code)
    {
        _logger.LogInformation("Request for deleting product with code {Code}", code);
        if (!Validator.isValidCode(code))
        {
            _logger.LogWarning("Invalid code recieved: {Code}.", code);
            return BadRequest("Invalid code format.");
        }
        try
        {
            await _productService.DeleteProduct(code);
            return NoContent();
        }
        catch (ProductNotFoundException e)
        {
            _logger.LogWarning("Product with code {Code} not found.", code);
            return NotFound(e.Message);
        }
        catch (DataAccessException e)
        {
            _logger.LogError(e, "Error while trying to delete product with code {Code}", code);
            return StatusCode(500, new { message = "An error occured during the request." });
        }
    }
}