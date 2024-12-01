using System.Text.RegularExpressions;
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
        var client = ClientMappings.ToDomainModel(clientDto);
        var createdClient = _clientService.AddClient(client);
        var responseDto = ClientMappings.ToResponseDto(createdClient);
        return CreatedAtAction(nameof(GetClient), new { nif = responseDto.Nif }, responseDto);
    }

    [HttpGet("{nif}")]
    public async Task<IActionResult> GetClient(string nif)
    {
        if (!IsValidNif(nif))
        {
            return BadRequest("Invalid NIF format.");
        }
        try
        {
            var client = await _clientService.GetClientByNif(nif);
            if (client == null)
            {
                return NotFound("Client not found");
            }
            var responseDto = ClientMappings.ToResponseDto(client);
            return Ok(responseDto);
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "An error occured during the request." });
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<ClientResponseDto>> GetClients()
    {
        var clients = _clientService.GetClients();
        var responseDtos = clients.Select(ClientMappings.ToResponseDto);
        return Ok(responseDtos);
    }

    [HttpPut("{nif}")]
    public ActionResult UpdateClient(string nif, ClientRequestDto clientDto)
    {
        if (nif != clientDto.Nif)
        {
            return BadRequest("Nif provided and client nif doesn't match.");
        }
        var client = ClientMappings.ToDomainModel(clientDto);
        var updatedClient = _clientService.UpdateClient(client);
        var responseDto = ClientMappings.ToResponseDto(updatedClient);
        return Ok(responseDto);
    }

    [HttpDelete("{nif}")]
    public IActionResult RemoveClient(string nif)
    {
        _clientService.RemoveClient(nif);
        return NoContent();
    }

    private bool IsValidNif(string nif)
    {
        return Regex.IsMatch(nif, @"^\d{8}[A-Z]{1}$");
    }
}