using Store.Repository.DbConfig;
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

    public ClientEntity? GetClientByNif(string nif)
    {
        return _context.Clients.FirstOrDefault(c => c.Nif == nif);
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