using AutoMapper;
using MediatR;
using CartaoVacina.Application.DTOs;
using CartaoVacina.Application.Interfaces;
using CartaoVacina.Domain.Entities;
using CartaoVacina.Domain.Exceptions;

namespace CartaoVacina.Application.Commands.Vacinas;

public class CreateVacinaCommandHandler : IRequestHandler<CreateVacinaCommand, VacinaDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateVacinaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<VacinaDto> Handle(CreateVacinaCommand request, CancellationToken cancellationToken)
    {
        // Verificar se o nome da vacina já existe
        var vacinaExistente = await _unitOfWork.Vacinas.GetByNomeAsync(request.Nome);
        if (vacinaExistente != null)
        {
            throw new DomainException("Já existe uma vacina cadastrada com este nome");
        }

        var vacina = new Vacina(request.Nome);
        
        await _unitOfWork.Vacinas.AddAsync(vacina);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<VacinaDto>(vacina);
    }
}
