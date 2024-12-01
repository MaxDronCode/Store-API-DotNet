using Store.Repository.Repositories;
using Store.Service.Mappings;
using Store.Service.Models;

namespace Store.Service.Services.Impl;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public Client AddClient(Client client)
    {
        var entity = ClientMappings.ToEntity(client);
        var addedEntity = _clientRepository.AddClient(entity);
        return ClientMappings.ToDomainModel(addedEntity);
    }

    public Client? GetClientByNif(string nif)
    {
        return ClientMappings.ToDomainModel(_clientRepository.GetClientByNif(nif));
    }

    public IEnumerable<Client> GetClients()
    {
        return _clientRepository.GetClients().Select(ClientMappings.ToDomainModel);
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