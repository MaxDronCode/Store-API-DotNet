using Store.Service.Models;

namespace Store.Service.Services;

public interface IClientService
{
    Client AddClient(Client client);

    Client? GetClientByNif(string nif);

    IEnumerable<Client> GetClients();

    void RemoveClient(string nif);

    Client UpdateClient(Client client);
}