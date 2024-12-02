using Store.Repository.Models;

namespace Store.Repository.Repositories;

public interface IClientRepository
{
    ClientEntity AddClient(ClientEntity client);

    Task<ClientEntity?> GetClientByNif(string nif);

    IEnumerable<ClientEntity> GetClients();

    Task RemoveClient(ClientEntity entity);

    Task<ClientEntity> UpdateClient(ClientEntity client);
}