using MediatR;
using Microsoft.AspNetCore.Mvc;
using CartaoVacina.Application.Commands.Pessoas;
using CartaoVacina.Application.DTOs;
using CartaoVacina.Application.Queries.Pessoas;
using FluentValidation;

namespace CartaoVacina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<CriarPessoaDto> _validator;

    public PessoasController(IMediator mediator, IValidator<CriarPessoaDto> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    /// <summary>
    /// Criar uma nova pessoa
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PessoaDto>> CreatePerson([FromBody] CriarPessoaDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Os dados da pessoa são obrigatórios.");
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

        var command = new CreatePessoaCommand
        {
            Nome = dto.Nome,
            Idade = dto.Idade,
            Sexo = dto.Sexo,
            Documento = dto.Documento
        };

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetPersonById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Buscar pessoa por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PessoaDto>> GetPersonById(Guid id)
    {
        var query = new GetPessoaByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound($"Pessoa com ID {id} não foi encontrada");

        return Ok(result);
    }

    // /// <summary>
    // /// Remover uma pessoa
    // /// </summary>
    // [HttpDelete("{id}")]
    // public async Task<ActionResult> DeletePessoa(Guid id)
    // {
    //     var command = new DeletePessoaCommand(id);
    //     await _mediator.Send(command);
    //     return NoContent();
    // }
}