using MediatR;
using CartaoVacina.Application.DTOs;
using CartaoVacina.Domain.Entities;

namespace CartaoVacina.Application.Commands.Pessoas;

public class CreatePessoaCommand : IRequest<PessoaDto>
{
    public required string Nome { get; set; }
    public required string Documento { get; set; }
    public required int Idade { get; set; }
    public required Pessoa.SexoPessoa Sexo { get; set; }
}