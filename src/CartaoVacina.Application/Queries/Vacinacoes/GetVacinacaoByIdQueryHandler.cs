using AutoMapper;
using MediatR;
using CartaoVacina.Application.DTOs;
using CartaoVacina.Application.Interfaces;
using CartaoVacina.Domain.Exceptions;

namespace CartaoVacina.Application.Queries.Vacinacoes
{
    public class GetVacinacaoByIdQueryHandler : IRequestHandler<GetVacinacaoByIdQuery, VacinacaoDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVacinacaoByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VacinacaoDto> Handle(GetVacinacaoByIdQuery request, CancellationToken cancellationToken)
        {
            var vacinacao = await _unitOfWork.Vacinacao.GetByIdAsync(request.Id);
            if (vacinacao == null)
            {
                throw new NotFoundException(request.Id,"Vacinação");
            }

            return _mapper.Map<VacinacaoDto>(vacinacao);
        }
    }
}