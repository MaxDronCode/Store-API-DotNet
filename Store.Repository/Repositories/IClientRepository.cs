using Store.Repository.Models;

namespace Store.Repository.Repositories;

public interface IClientRepository
{
    ClientEntity AddClient(ClientEntity client);

    Task<ClientEntity?> GetClientByNif(string nif);

    IEnumerable<ClientEntity> GetClients();

    void RemoveClient(string nif);

    Task<ClientEntity> UpdateClient(ClientEntity client);
}