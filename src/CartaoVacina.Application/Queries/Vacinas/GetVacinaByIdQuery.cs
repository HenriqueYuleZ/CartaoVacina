using MediatR;
using CartaoVacina.Application.DTOs;

namespace CartaoVacina.Application.Queries.Vacinas;

public class GetVacinaByIdQuery : IRequest<VacinaDto?>
{
    public Guid Id { get; set; }

    public GetVacinaByIdQuery(Guid id)
    {
        Id = id;
    }
}
