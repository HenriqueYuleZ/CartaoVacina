using AutoMapper;
using MediatR;
using CartaoVacina.Application.DTOs;
using CartaoVacina.Application.Interfaces;

namespace CartaoVacina.Application.Queries.Vacinas;

public class GetAllVacinasQueryHandler : IRequestHandler<GetAllVacinasQuery, List<VacinaDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllVacinasQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<VacinaDto>> Handle(GetAllVacinasQuery request, CancellationToken cancellationToken)
    {
        var vacinas = await _unitOfWork.Vacinas.GetAllAsync();
        return _mapper.Map<List<VacinaDto>>(vacinas);
    }
}
