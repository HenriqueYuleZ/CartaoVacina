using AutoMapper;
using MediatR;
using CartaoVacina.Application.DTOs;
using CartaoVacina.Application.Interfaces;

namespace CartaoVacina.Application.Queries.Pessoas;

public class GetPessoaByIdQueryHandler : IRequestHandler<GetPessoaByIdQuery, PessoaDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPessoaByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PessoaDto?> Handle(GetPessoaByIdQuery request, CancellationToken cancellationToken)
    {
        var pessoa = await _unitOfWork.Pessoas.GetByIdAsync(request.Id);
        return pessoa == null ? null : _mapper.Map<PessoaDto>(pessoa);
    }
}