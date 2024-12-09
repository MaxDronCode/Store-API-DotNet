using Microsoft.Extensions.Logging;
using Store.Api.Exceptions;
using Store.Exceptions;
using Store.Repository;
using Store.Repository.Repositories;
using Store.Service.Mappings;
using Store.Service.Models;

namespace Store.Service.Services.Impl;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IClientRepository clientRepository;
    private readonly IProductRepository productRepository;
    private readonly ILogger<SaleService> _logger;

    public SaleService(ISaleRepository saleRepository, IClientRepository clientRepository, IProductRepository productRepository, ILogger<SaleService> logger)
    {
        _saleRepository = saleRepository;
        this.clientRepository = clientRepository;
        this.productRepository = productRepository;
        _logger = logger;
    }

    public async Task<Sale> CreateSale(Sale sale)
    {
        var client = await clientRepository.GetClientByNif(sale.ClientNif);

        if (client == null)
        {
            _logger.LogError($"Client with NIF {sale.ClientNif} not found");
            throw new ClientNotFoundException($"Client with nif {sale.ClientNif} not found.");
        }

        foreach (var item in sale.Items)
        {
            var product = await productRepository.GetProductByName(item.ProductName);

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
}