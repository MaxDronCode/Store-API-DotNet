using Microsoft.AspNetCore.Mvc;
using Store.Api.Exceptions;
using Store.Api.Mappings;
using Store.Api.Models;
using Store.Exceptions;
using Store.Service.Models;
using Store.Service.Services;

namespace Store.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly ILogger<SalesController> _logger;

    public SalesController(ISaleService saleService, ILogger<SalesController> logger)
    {
        _saleService = saleService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSale(SaleRequestDto saleDto)
    {
        var sale = SaleMappings.ToModel(saleDto);
        Sale createdSale;
        try
        {
            createdSale = await _saleService.CreateSale(sale);
        }
        catch (ClientNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ProductNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating sale");
            return StatusCode(500, e.Message);
        }

        var responseDto = SaleMappings.ToDto(createdSale);

        return CreatedAtAction(nameof(CreateSale), responseDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetSalesByClientNif(string nif)
    {
        _logger.LogInformation("Request for obtaining sales for client with NIF {Nif}", nif);
        if (!Helpers.Validator.IsValidNif(nif))
        {
            _logger.LogWarning("Invalid NIF recieved: {Nif}.", nif);
            return BadRequest("Invalid NIF format.");
        }
        try
        {
            var sales = await _saleService.GetSalesByClientNif(nif);
            if (sales == null)
            {
                return NotFound();
            }
            var response = sales.Select(s => new SaleResponseDto
            {
                Id = s.Id,
                ClientNif = s.ClientNif,
                Items = s.Items.Select(i => new SaleItemResponseDto
                {
                    ProductName = i.ProductName,
                    Quantity = i.Quantity
                }).ToList()
            });

            return Ok(response);
        }
        catch (ClientNotFoundException e)
        {
            _logger.LogError(e, "Client with NIF {Nif} not found", nif);
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error obtaining sales for client with NIF {Nif}", nif);
            return StatusCode(500, e.Message);
        }
    }
}