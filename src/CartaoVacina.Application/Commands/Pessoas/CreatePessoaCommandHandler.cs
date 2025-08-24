using AutoMapper;
using MediatR;
using CartaoVacina.Application.DTOs;
using CartaoVacina.Application.Interfaces;
using CartaoVacina.Domain.Entities;
using VaccinationCard.Domain.Exceptions;

namespace CartaoVacina.Application.Commands.Pessoas;

public class CreatePessoaCommandHandler : IRequestHandler<CreatePessoaCommand, PessoaDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePessoaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PessoaDto> Handle(CreatePessoaCommand request, CancellationToken cancellationToken)
    {
        // Verificar se o documento já existe
        if (await _unitOfWork.Pessoas.DocumentoExisteAsync(request.Documento))
        {
            throw new DomainException("Já existe uma pessoa cadastrada com este número de documento");
        }

        var pessoa = new Pessoa(request.Nome, request.Documento);
        
        await _unitOfWork.Pessoas.AddAsync(pessoa);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PessoaDto>(pessoa);
    }
}