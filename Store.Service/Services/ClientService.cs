using Store.Service.Models;

namespace Store.Service.Services;

public interface IClientService
{
    Task<Client> AddClient(Client client);

    Task<Client?> GetClientByNif(string nif);

    IEnumerable<Client> GetClients();

    Task RemoveClient(string nif);

    Task<Client> UpdateClient(Client client);
}