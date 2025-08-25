using MediatR;

namespace CartaoVacina.Application.Commands.Pessoas;
public class DeletePessoaCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}