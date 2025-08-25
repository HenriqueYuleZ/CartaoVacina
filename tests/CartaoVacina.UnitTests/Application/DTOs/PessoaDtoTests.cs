using CartaoVacina.Application.DTOs;
using static CartaoVacina.Domain.Entities.Pessoa;

namespace CartaoVacina.UnitTests.Application.DTOs;

public class PessoaDtoTests
{
    [Fact]
    public void PessoaDto_DeveSerInstanciada_ComPropriedadesCorretas()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "João Silva";
        var idade = 30;
        var sexo = SexoPessoa.Masculino;
        var documento = "12345678901";
        var createdAt = DateTime.UtcNow;

        // Act
        var dto = new PessoaDto
        {
            Id = id,
            Nome = nome,
            Idade = idade,
            Sexo = sexo,
            Documento = documento,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(nome, dto.Nome);
        Assert.Equal(idade, dto.Idade);
        Assert.Equal(sexo, dto.Sexo);
        Assert.Equal(documento, dto.Documento);
        Assert.Equal(createdAt, dto.CreatedAt);
    }

    [Fact]
    public void CriarPessoaDto_DeveSerInstanciada_ComPropriedadesCorretas()
    {
        // Arrange
        var nome = "João Silva";
        var idade = 30;
        var sexo = SexoPessoa.Feminino;
        var documento = "12345678901";

        // Act
        var dto = new CriarPessoaDto
        {
            Nome = nome,
            Idade = idade,
            Sexo = sexo,
            Documento = documento
        };

        // Assert
        Assert.Equal(nome, dto.Nome);
        Assert.Equal(idade, dto.Idade);
        Assert.Equal(sexo, dto.Sexo);
        Assert.Equal(documento, dto.Documento);
    }

    [Theory]
    [InlineData(SexoPessoa.Masculino)]
    [InlineData(SexoPessoa.Feminino)]
    [InlineData(SexoPessoa.Outro)]
    public void PessoaDto_DeveAceitarTodosOsValoresDeSexo(SexoPessoa sexo)
    {
        // Arrange & Act
        var dto = new PessoaDto
        {
            Id = Guid.NewGuid(),
            Nome = "João Silva",
            Idade = 30,
            Sexo = sexo,
            Documento = "12345678901",
            CreatedAt = DateTime.UtcNow
        };

        // Assert
        Assert.Equal(sexo, dto.Sexo);
    }

    [Theory]
    [InlineData(SexoPessoa.Masculino)]
    [InlineData(SexoPessoa.Feminino)]
    [InlineData(SexoPessoa.Outro)]
    public void CriarPessoaDto_DeveAceitarTodosOsValoresDeSexo(SexoPessoa sexo)
    {
        // Arrange & Act
        var dto = new CriarPessoaDto
        {
            Nome = "João Silva",
            Idade = 30,
            Sexo = sexo,
            Documento = "12345678901"
        };

        // Assert
        Assert.Equal(sexo, dto.Sexo);
    }
}
