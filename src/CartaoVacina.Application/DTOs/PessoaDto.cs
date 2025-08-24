using CartaoVacina.Domain.Entities;

namespace CartaoVacina.Application.DTOs;

public class PessoaDto
{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public required int Idade { get; set; }
    public Pessoa.SexoPessoa Sexo { get; set; }
    public required string Documento { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CriarPessoaDto
{
    public required string Nome { get; set; }
    public required int Idade { get; set; }
    public required Pessoa.SexoPessoa Sexo { get; set; }
    public required string Documento { get; set; }
}