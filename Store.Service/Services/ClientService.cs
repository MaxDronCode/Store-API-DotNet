using Store.Service.Models;

namespace Store.Service.Services;

public interface IClientService
{
    Client AddClient(Client client);

    Task<Client?> GetClientByNif(string nif);

    IEnumerable<Client> GetClients();

    void RemoveClient(string nif);

    Task<Client> UpdateClient(Client client);
}