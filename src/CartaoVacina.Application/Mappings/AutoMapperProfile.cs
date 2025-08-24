using AutoMapper;
using CartaoVacina.Application.Commands.Pessoas;
using CartaoVacina.Application.Commands.Vacinas;
using CartaoVacina.Application.DTOs;
using CartaoVacina.Domain.Entities;

namespace CartaoVacina.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Person Mappings
        CreateMap<Pessoa, PessoaDto>();
        CreateMap<CriarPessoaDto, CreatePessoaCommand>();

        // Vacina Mappings
        CreateMap<Vacina, VacinaDto>();
        CreateMap<CriarVacinaDto, CreateVacinaCommand>();
    }
}