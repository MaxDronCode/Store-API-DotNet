using Microsoft.Extensions.Logging;
using Store.Api.Exceptions;
using Store.Exceptions;
using Store.Repository;
using Store.Repository.Models;
using Store.Repository.Repositories;
using Store.Service.Mappings;
using Store.Service.Models;

namespace Store.Service.Services.Impl;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IProductRepository _productRepository;
    private readonly ILogger<SaleService> _logger;

    public SaleService(ISaleRepository saleRepository, IClientRepository clientRepository, IProductRepository productRepository, ILogger<SaleService> logger)
    {
        _saleRepository = saleRepository;
        _clientRepository = clientRepository;
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task<Sale> CreateSale(Sale sale)
    {
        var client = await _clientRepository.GetClientByNif(sale.ClientNif);

        if (client == null)
        {
            _logger.LogError($"Client with NIF {sale.ClientNif} not found");
            throw new ClientNotFoundException($"Client with nif {sale.ClientNif} not found.");
        }

        foreach (var item in sale.Items)
        {
            var product = await _productRepository.GetProductByName(item.ProductName);

            if (product == null)
            {
                _logger.LogError($"Product with name {item.ProductName} not found");
                throw new ProductNotFoundException($"Product with name {item.ProductName} not found.");
            }

            item.ProductCode = product.Code;
        }

        var saleEntity = SaleMappings.ToEntity(sale);

        var createdSaleEntity = await _saleRepository.CreateSale(saleEntity);

        var createdSale = SaleMappings.ToModel(createdSaleEntity);

        foreach (var item in createdSale.Items)
        {
            var originalItem = sale.Items.FirstOrDefault(i => i.ProductCode == item.ProductCode);
            if (originalItem != null)
                item.ProductName = originalItem.ProductName;
        }

        return createdSale;
    }

    public async Task<IEnumerable<Sale>> GetSalesByClientNif(string clientNif)
    {
        var client = await _clientRepository.GetClientByNif(clientNif);

        if (client == null)
        {
            _logger.LogError($"Client with NIF {clientNif} not found");
            throw new ClientNotFoundException($"Client with nif {clientNif} not found.");
        }
        IEnumerable<SaleEntity> saleEntities;
        try
        {
            saleEntities = await _saleRepository.GetSalesByClientNif(clientNif);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while trying to get sales by client NIF {Nif}", clientNif);
            throw;
        }

        var sales = saleEntities.Select(s => new Sale
        {
            Id = s.Id,
            ClientNif = s.ClientNif,
            Items = s.SaleDetails.Select(sd => new SaleDetail
            {
                ProductCode = sd.ProductCode,
                ProductName = sd.Product?.Name ?? string.Empty,
                Quantity = sd.Quantity
            }).ToList()
        }).ToList();

        return sales;
    }
}