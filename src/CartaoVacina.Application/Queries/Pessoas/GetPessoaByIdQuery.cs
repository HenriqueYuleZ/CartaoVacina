using MediatR;
using CartaoVacina.Application.DTOs;

namespace CartaoVacina.Application.Queries.Pessoas;

public class GetPessoaByIdQuery : IRequest<PessoaDto?>
{
    public Guid Id { get; set; }

    public GetPessoaByIdQuery(Guid id)
    {
        Id = id;
    }
}