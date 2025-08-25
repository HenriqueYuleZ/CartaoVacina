using AutoMapper;
using MediatR;
using CartaoVacina.Application.DTOs;
using CartaoVacina.Application.Interfaces;
using CartaoVacina.Domain.Entities;
using CartaoVacina.Domain.Exceptions;

namespace CartaoVacina.Application.Commands.Vacinacoes;
public class CreateVacinacaoCommandHandler : IRequestHandler<CreateVacinacaoCommand, VacinacaoDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateVacinacaoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<VacinacaoDto> Handle(CreateVacinacaoCommand request, CancellationToken cancellationToken)
    {
        var vacinacao = new Vacinacao(
            request.PessoaId,
            request.VacinaId,
            request.Dose,
            request.DataAplicacao
        );

        await _unitOfWork.Vacinacao.AddAsync(vacinacao);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<VacinacaoDto>(vacinacao);
    }
}