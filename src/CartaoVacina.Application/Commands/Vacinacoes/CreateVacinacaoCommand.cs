using MediatR;
using CartaoVacina.Application.DTOs;

namespace CartaoVacina.Application.Commands.Vacinacoes
{
    public class CreateVacinacaoCommand : IRequest<VacinacaoDto>
    {
        public Guid PessoaId { get; set; }
        public Guid VacinaId { get; set; }
        public int Dose { get; set; }
        public DateTime DataAplicacao { get; set; }
    }
}