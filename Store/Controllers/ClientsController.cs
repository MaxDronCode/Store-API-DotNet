﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Exceptions;
using Store.Api.Mappings;
using Store.API.Models;
using Store.Exceptions;
using Store.Service.Models;
using Store.Service.Services;

namespace Store.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly ILogger<ClientsController> _logger;

    public ClientsController(IClientService clientService, ILogger<ClientsController> logger)
    {
        _clientService = clientService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient(ClientRequestDto clientDto)
    {
        var client = ClientMappings.ToDomainModel(clientDto);
        Client createdClient;
        try
        {
            createdClient = await _clientService.AddClient(client);
        }
        catch (ClientAlreadyExistsException e)
        {
            _logger.LogWarning(e, "Client with NIF {Nif} already exists.", client.Nif);
            return Conflict(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating client with NIF {Nif}", client.Nif);
            return StatusCode(500, new { message = "An error occured during the request." });
        }
        var responseDto = ClientMappings.ToResponseDto(createdClient);
        return CreatedAtAction(nameof(GetClient), new { nif = responseDto.Nif }, responseDto);
    }

    [HttpGet("{nif}")]
    public async Task<IActionResult> GetClient(string nif)
    {
        _logger.LogInformation("Request for obtaining client with NIF {Nif}", nif);

        if (!Helpers.Validator.IsValidNif(nif))
        {
            _logger.LogWarning("Invalid NIF recieved: {Nif}.", nif);
            return BadRequest("Invalid NIF format.");
        }
        try
        {
            var client = await _clientService.GetClientByNif(nif);
            if (client == null)
            {
                _logger.LogInformation("Client with NIF {Nif} not found.", nif);
                return NotFound("Client not found");
            }
            var responseDto = ClientMappings.ToResponseDto(client);
            return Ok(responseDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error obtaining client with NIF {Nif}", nif);
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
    public async Task<ActionResult> UpdateClient(string nif, ClientRequestDto clientDto)
    {
        _logger.LogInformation("Request for updating client with NIF {Nif}", nif);

        if (!Helpers.Validator.IsValidNif(nif))
        {
            ModelState.AddModelError("InvalidNif", "Invalid NIF format.");
            return BadRequest(ModelState);
        }

        if (nif != clientDto.Nif)
        {
            ModelState.AddModelError("NifMismatch", "Nif provided and client nif doesn't match.");
            return BadRequest(ModelState);
        }

        var client = ClientMappings.ToDomainModel(clientDto);

        try
        {
            var updatedClient = await _clientService.UpdateClient(client);
            var responseDto = ClientMappings.ToResponseDto(updatedClient);
            return Ok(responseDto);
        }
        catch (ClientNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating client with NIF {Nif}", nif);
            return StatusCode(500, new { message = "An error occured during the request." });
        }
    }

    [HttpDelete("{nif}")]
    public async Task<IActionResult> RemoveClient(string nif)
    {
        _logger.LogInformation("Request for removing client with NIF {Nif}", nif);

        if (!Helpers.Validator.IsValidNif(nif))
        {
            ModelState.AddModelError("InvalidNif", "Invalid NIF format.");
            return BadRequest(ModelState);
        }

        try
        {
            await _clientService.RemoveClient(nif);
            return NoContent();
        }
        catch (ClientNotFoundException e)
        {
            _logger.LogWarning(e, "Client with NIF {Nif} not found.", nif);
            return NotFound(e.Message);
        }
        catch (ValidationException e)
        {
            _logger.LogWarning(e, "Validation error while trying to remove client with NIF {Nif}", nif);
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error removing client with NIF {Nif}", nif);
            return StatusCode(500, new { message = "An error occured during the request." });
        }
    }
}