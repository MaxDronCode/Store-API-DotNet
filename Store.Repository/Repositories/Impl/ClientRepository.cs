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

    public ClientEntity AddClient(ClientEntity client)
    {
        _context.Clients.Add(client);
        _context.SaveChanges();
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

    public void RemoveClient(string nif)
    {
        throw new NotImplementedException();
    }

    public ClientEntity UpdateClient(ClientEntity client)
    {
        throw new NotImplementedException();
    }
}