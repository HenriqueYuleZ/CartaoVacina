using MediatR;
using CartaoVacina.Application.DTOs;

namespace CartaoVacina.Application.Queries.Pessoas;

public class GetAllPessoasQuery : IRequest<List<PessoaDto>>
{
}
