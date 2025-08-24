using MediatR;
using Microsoft.AspNetCore.Mvc;
using CartaoVacina.Application.Commands.Vacinas;
using CartaoVacina.Application.DTOs;
using CartaoVacina.Application.Queries.Vacinas;
using FluentValidation;

namespace CartaoVacina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VacinasController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<CriarVacinaDto> _validator;

    public VacinasController(IMediator mediator, IValidator<CriarVacinaDto> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    /// <summary>
    /// Criar uma nova vacina
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<VacinaDto>> CreateVacina([FromBody] CriarVacinaDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Os dados da vacina são obrigatórios.");
        }

        // Validação com FluentValidation
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new 
            { 
                Property = e.PropertyName, 
                Error = e.ErrorMessage 
            }));
        }

        var command = new CreateVacinaCommand
        {
            Nome = dto.Nome
        };

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetVacinaById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Buscar vacina por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<VacinaDto>> GetVacinaById(Guid id)
    {
        var query = new GetVacinaByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound($"Vacina com ID {id} não foi encontrada");

        return Ok(result);
    }
}
