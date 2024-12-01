using Store.API.Models;
using Store.Service.Models;

namespace Store.Api.Mappings;

public static class ClientMappings
{
    public static Client ToDomainModel(ClientRequestDto dto)
    {
        return new Client
        {
            Nif = dto.Nif,
            Name = dto.Name,
            Address = dto.Address
        };
    }

    public static ClientResponseDto ToResponseDto(Client domain)
    {
        return new ClientResponseDto
        {
            Nif = domain.Nif,
            Name = domain.Name,
            Address = domain.Address
        };
    }
}