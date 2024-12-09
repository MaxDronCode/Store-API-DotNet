using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Store.Api.Exceptions;
using Store.Exceptions;
using Store.Repository.Exceptions;
using Store.Repository.Repositories;
using Store.Service.Mappings;
using Store.Service.Models;

namespace Store.Service.Services.Impl;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly ILogger<ClientService> _logger;
    private readonly IMemoryCache _cache;

    public ClientService(IClientRepository clientRepository, ILogger<ClientService> logger, IMemoryCache cache)
    {
        _clientRepository = clientRepository;
        _logger = logger;
        _cache = cache;
    }

    public async Task<Client> AddClient(Client client)
    {
        var existingClient = await GetClientByNif(client.Nif);
        if (existingClient != null)
        {
            throw new ClientAlreadyExistsException($"Client already exists with NIF {client.Nif}");
        }

        try
        {
            var entity = ClientMappings.ToEntity(client);
            var addedEntity = await _clientRepository.AddClient(entity);
            return ClientMappings.ToDomainModel(addedEntity);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while trying to add client with NIF {Nif}", client.Nif);
            throw;
        }
    }

    public async Task<Client?> GetClientByNif(string nif)
    {
        _logger.LogInformation("Getting client by NIF {Nif}", nif);

        var cacheKey = $"Client_{nif}";
        if (_cache.TryGetValue(cacheKey, out Client? cachedClient))
        {
            _logger.LogInformation("Client with NIF {Nif} obtaind from caché.", nif);
            return cachedClient;
        }
        var clientEntity = await _clientRepository.GetClientByNif(nif);
        var client = clientEntity != null ? ClientMappings.ToDomainModel(clientEntity) : null;

        if (client != null)
        {
            _cache.Set(cacheKey, client, TimeSpan.FromMinutes(30));
        }

        return client;
    }

    public IEnumerable<Client> GetClients()
    {
        return _clientRepository.GetClients().Select(ClientMappings.ToDomainModel);
    }

    public async Task RemoveClient(string nif)
    {
        _logger.LogInformation("Removing client with NIF {Nif}", nif);

        var clientEntity = await _clientRepository.GetClientByNif(nif);
        if (clientEntity == null)
        {
            throw new ClientNotFoundException($"Client not found with NIF {nif}");
        }

        try
        {
            await _clientRepository.RemoveClient(clientEntity);
        }
        catch (DataAccessException e)
        {
            _logger.LogError(e, "Error while trying to remove client with NIF {Nif}", nif);
            throw;
        }
    }

    public async Task<Client> UpdateClient(Client client)
    {
        var existingEntity = await _clientRepository.GetClientByNif(client.Nif);
        if (existingEntity == null)
        {
            throw new ClientNotFoundException("Client not found with NIF {client.Nif}");
        }

        existingEntity.Name = client.Name;
        existingEntity.Address = client.Address;

        try
        {
            var updatedEntity = await _clientRepository.UpdateClient(existingEntity);
            return ClientMappings.ToDomainModel(updatedEntity);
        }
        catch (DataAccessException e)
        {
            _logger.LogError(e, "Error while trying to update client with NIF {Nif}", client.Nif);
            throw;
        }
    }
}