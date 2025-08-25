using MediatR;
using CartaoVacina.Application.DTOs;

namespace CartaoVacina.Application.Queries.Vacinacoes
{
    public class GetCartaoVacinacaoByPessoaIdQuery : IRequest<CartaoVacinacaoDto>
    {
        public Guid PessoaId { get; set; }

        public GetCartaoVacinacaoByPessoaIdQuery(Guid pessoaId)
        {
            PessoaId = pessoaId;
        }
    }
}
