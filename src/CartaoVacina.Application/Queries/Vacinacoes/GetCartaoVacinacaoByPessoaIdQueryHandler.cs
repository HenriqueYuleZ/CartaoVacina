using AutoMapper;
using MediatR;
using CartaoVacina.Application.DTOs;
using CartaoVacina.Application.Interfaces;
using CartaoVacina.Domain.Exceptions;

namespace CartaoVacina.Application.Queries.Vacinacoes
{
    public class GetCartaoVacinacaoByPessoaIdQueryHandler : IRequestHandler<GetCartaoVacinacaoByPessoaIdQuery, CartaoVacinacaoDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCartaoVacinacaoByPessoaIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CartaoVacinacaoDto> Handle(GetCartaoVacinacaoByPessoaIdQuery request, CancellationToken cancellationToken)
        {
            // Buscar a pessoa
            var pessoa = await _unitOfWork.Pessoas.GetByIdAsync(request.PessoaId);
            if (pessoa == null)
            {
                throw new NotFoundException(request.PessoaId, "Pessoa");
            }

            // Buscar todas as vacinações da pessoa
            var vacinacoes = await _unitOfWork.Vacinacao.GetAllByPessoaIdAsync(request.PessoaId);

            var cartaoVacinacao = new CartaoVacinacaoDto
            {
                Pessoa = _mapper.Map<PessoaDto>(pessoa),
                Vacinacoes = vacinacoes.Select(v => new VacinacaoDetalhadaDto
                {
                    Id = v.Id,
                    Dose = v.Dose,
                    DataAplicacao = v.DataAplicacao,
                    Vacina = _mapper.Map<VacinaDto>(v.Vacina)
                }).ToList()
            };

            return cartaoVacinacao;
        }
    }
}
