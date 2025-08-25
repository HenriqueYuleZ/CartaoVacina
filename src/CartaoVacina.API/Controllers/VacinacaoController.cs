using MediatR;
using Microsoft.AspNetCore.Mvc;
using CartaoVacina.Application.Commands.Vacinacoes;
using CartaoVacina.Application.DTOs;
using CartaoVacina.Application.Queries.Vacinacoes;
using FluentValidation;

namespace CartaoVacina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VacinacaoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<CriarVacinacaoDto> _validator;

    public VacinacaoController(IMediator mediator, IValidator<CriarVacinacaoDto> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    /// <summary>
    /// Criar uma nova vacinação
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<VacinacaoDto>> CreateVacinacao([FromBody] CriarVacinacaoDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Os dados da vacinação são obrigatórios.");
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

        var command = new CreateVacinacaoCommand
        {
            PessoaId = dto.PessoaId,
            VacinaId = dto.VacinaId,
            Dose = dto.Dose,
            DataAplicacao = dto.DataAplicacao
        };

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetVacinacaoById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Buscar vacinação por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<VacinacaoDto>> GetVacinacaoById(Guid id)
    {
        var query = new GetVacinacaoByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound($"Vacinação com ID {id} não foi encontrada");

        return Ok(result);
    }

    /// <summary>
    /// Buscar cartão de vacinação completo por ID da pessoa
    /// </summary>
    [HttpGet("cartao-vacinacao")]
    public async Task<ActionResult<CartaoVacinacaoDto>> GetCartaoVacinacao([FromQuery] Guid pessoaId)
    {
        var query = new GetCartaoVacinacaoByPessoaIdQuery(pessoaId);
        var result = await _mediator.Send(query);

        return Ok(result);
    }
}
