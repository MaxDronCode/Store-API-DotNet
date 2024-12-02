using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Repository.DbConfig;
using Store.Repository.Exceptions;
using Store.Repository.Models;

namespace Store.Repository.Repositories.Impl;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<ClientRepository> _logger;

    public ClientRepository(AppDbContext context, ILogger<ClientRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ClientEntity> AddClient(ClientEntity client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<ClientEntity?> GetClientByNif(string nif)
    {
        try
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Nif == nif);
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e, "Error while trying to get client by NIF {Nif}", nif);
            throw new DataAccessException("Error while trying to get client by NIF", e);
        }
    }

    public IEnumerable<ClientEntity> GetClients()
    {
        return _context.Clients;
    }

    public async Task RemoveClient(ClientEntity entity)
    {
        try
        {
            _context.Clients.Remove(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Client with NIF {Nif} removed.", entity.Nif);
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e, "Error while trying to remove client with NIF {Nif}", entity.Nif);
            throw new DataAccessException("Error while trying to remove client", e);
        }
    }

    public async Task<ClientEntity> UpdateClient(ClientEntity client)
    {
        try
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Client with NIF {Nif} updated.", client.Nif);
            return client;
        }
        catch (DbUpdateConcurrencyException e)
        {
            _logger.LogError(e, "Concurreny error while trying to update client with NIF {Nif}", client.Nif);
            throw new DataAccessException("Error while trying to update client", e);
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e, "Error while trying to update client with NIF {Nif}", client.Nif);
            throw new DataAccessException("Error while trying to update client", e);
        }
    }
}