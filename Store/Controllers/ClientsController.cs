using Microsoft.AspNetCore.Mvc;
using Store.Api.Mappings;
using Store.API.Models;
using Store.Service.Services;

namespace Store.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost]
    public IActionResult CreateClient(ClientRequestDto clientDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var client = ClientMappings.ToDomainModel(clientDto);
        var createdClient = _clientService.AddClient(client);
        var responseDto = ClientMappings.ToResponseDto(createdClient);
        return CreatedAtAction(nameof(GetClient), new { nif = responseDto.Nif }, responseDto);
    }

    [HttpGet("{nif}")]
    public IActionResult GetClient(string nif)
    {
        var client = _clientService.GetClientByNif(nif);
        if (client == null)
        {
            return NotFound();
        }
        var responseDto = ClientMappings.ToResponseDto(client);
        return Ok(responseDto);
    }
}