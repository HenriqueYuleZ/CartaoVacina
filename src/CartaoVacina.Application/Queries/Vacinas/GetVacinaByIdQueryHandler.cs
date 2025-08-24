using AutoMapper;
using MediatR;
using CartaoVacina.Application.DTOs;
using CartaoVacina.Application.Interfaces;

namespace CartaoVacina.Application.Queries.Vacinas;

public class GetVacinaByIdQueryHandler : IRequestHandler<GetVacinaByIdQuery, VacinaDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetVacinaByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<VacinaDto?> Handle(GetVacinaByIdQuery request, CancellationToken cancellationToken)
    {
        var vacina = await _unitOfWork.Vacinas.GetByIdAsync(request.Id);
        return vacina == null ? null : _mapper.Map<VacinaDto>(vacina);
    }
}
