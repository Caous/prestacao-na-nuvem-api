﻿using Newtonsoft.Json;

namespace PrestacaoNuvem.Api.Controllers;

/// <summary>
/// Controller Domínio LeadGoogle
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class LeadGoogleInnovaController : MainController
{
    private readonly ILeadGoogleService _service;

    public LeadGoogleInnovaController(ILeadGoogleService service)
    {
        _service = service;
    }

    [HttpGet("GetAllLeadGoogle")]
    public async Task<IActionResult> GetAllLeadGoogleAsync()
    {
        try
        {
            ICollection<LeadGoogleDtoResponse> classes = await _service.GetAllAsync();

            if (classes == null)
                return NoContent();

            return Ok(classes);
        }
        catch (Exception ex)
        {

            return StatusCode(500, ex);
        }

    }

    [HttpPost("PostLeadGoogleEmail")]
    public async Task<IActionResult> PostLeadGoogleEmailAsync(EmailRequestDto request)
    {
        var result = await _service.PostLeadEmailAsync(request);

        if (result != null)
            return Ok();
        else
            return BadRequest("Usuário já existente");
    }

    [HttpPost("PostLeadGoogle")]
    public async Task<IActionResult> PostLeadGoogleAsync(LeadGoogleDtoRequest request)
    {
        var result = await _service.PostLeadAsync(request);

        if (result != null)
            return Ok();
        else
            return BadRequest("Usuário já existente");
    }

    [HttpPut("PutLeadGoogle")]
    public async Task<IActionResult> PutLeadGoogleAsync(LeadGoogleDtoRequest request)
    {
        string output = JsonConvert.SerializeObject(request);
        var result = await _service.PutLeadAsync(request);

        if (result != null)
            return Ok();
        else
            return BadRequest("Usuário já existente");
    }

    [HttpGet("GetLeadByFilter/{nameEntreprise}/{category}/{phoneNumber}")]
    public async Task<IActionResult> GetAllLeadGoogleAsync(string nameEntreprise, string category, string phoneNumber)
    {
        LeadGoogleDtoResponse classes = await _service.GetAllLeadGoogleAsync(new LeadGoogleDtoRequest() { Name = nameEntreprise, Category = category, PhoneNumber = phoneNumber });

        if (classes == null)
            return NoContent();

        return Ok(classes);
    }

    [HttpDelete("DeleteLeadGoogle/{id}")]
    public async Task<IActionResult> DeleteLeadGoogle(string id)
    {
        var result = await _service.DeleteLeadGoogle(new MongoDB.Bson.ObjectId(id));

        if (result)
            return Ok();
        else
            return BadRequest("Usuário já existente");
    }
}
