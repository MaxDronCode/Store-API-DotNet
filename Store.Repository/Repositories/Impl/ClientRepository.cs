using Microsoft.EntityFrameworkCore;
using Store.Repository.DbConfig;
using Store.Repository.Exceptions;
using Store.Repository.Models;

namespace Store.Repository.Repositories.Impl;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
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
            return await _context.Clients.FirstOrDefault(c => c.Nif == nif);
        }
        catch (DbUpdateException e)
        {
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