using MediatR;
using CartaoVacina.Application.DTOs;

namespace CartaoVacina.Application.Queries.Vacinacoes
{
    public class GetVacinacaoByIdQuery : IRequest<VacinacaoDto>
    {
        public Guid Id { get; set; }

        public GetVacinacaoByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}