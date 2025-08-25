using AutoMapper;
using MediatR;
using CartaoVacina.Application.DTOs;
using CartaoVacina.Application.Interfaces;

namespace CartaoVacina.Application.Queries.Pessoas;

public class GetAllPessoasQueryHandler : IRequestHandler<GetAllPessoasQuery, List<PessoaDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllPessoasQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<PessoaDto>> Handle(GetAllPessoasQuery request, CancellationToken cancellationToken)
    {
        var pessoas = await _unitOfWork.Pessoas.GetAllAsync();
        return _mapper.Map<List<PessoaDto>>(pessoas);
    }
}
