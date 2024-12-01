using Store.Repository.Models;

namespace Store.Repository.Repositories;

public interface IClientRepository
{
    ClientEntity AddClient(ClientEntity client);

    ClientEntity? GetClientByNif(string nif);

    IEnumerable<ClientEntity> GetClients();

    void RemoveClient(string nif);

    ClientEntity UpdateClient(ClientEntity client);
}