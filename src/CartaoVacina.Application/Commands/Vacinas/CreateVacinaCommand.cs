using MediatR;
using CartaoVacina.Application.DTOs;

namespace CartaoVacina.Application.Commands.Vacinas;

public class CreateVacinaCommand : IRequest<VacinaDto>
{
    public required string Nome { get; set; }
}
