using MediatR;
using CartaoVacina.Application.DTOs;

namespace CartaoVacina.Application.Queries.Vacinas;

public class GetAllVacinasQuery : IRequest<List<VacinaDto>>
{
}
