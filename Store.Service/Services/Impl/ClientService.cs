using Store.Repository.Repositories;
using Store.Service.Mappings;
using Store.Service.Models;

namespace Store.Service.Services.Impl;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public Client AddClient(Client client)
    {
        var entity = ClientMappings.ToEntity(client);
        var addedEntity = _clientRepository.AddClient(entity);
        return ClientMappings.ToDomainModel(addedEntity);
    }

    public Client? GetClientByNif(string nif)
    {
        var entity = _clientRepository.GetClientByNif(nif);
        return entity is null ? null : ClientMappings.ToDomainModel(entity);
    }

    public IEnumerable<Client> GetClients()
    {
        throw new NotImplementedException();
    }

    public void RemoveClient(string nif)
    {
        throw new NotImplementedException();
    }

    public Client UpdateClient(Client client)
    {
        throw new NotImplementedException();
    }
}