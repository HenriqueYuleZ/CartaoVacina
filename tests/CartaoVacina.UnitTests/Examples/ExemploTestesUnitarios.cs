using CartaoVacina.Domain.Entities;

namespace CartaoVacina.UnitTests.Examples;

/// <summary>
/// Exemplo de como criar testes unitários seguindo os padrões do projeto
/// </summary>
public class ExemploTestesUnitarios
{
    [Fact]
    public void ExemploTeste_Simples()
    {
        // Arrange - Preparar dados de entrada
        var nome = "João Silva";
        var documento = "12345678901";
        var idade = 30;
        var sexo = Pessoa.SexoPessoa.Masculino;

        // Act - Executar a ação
        var pessoa = new Pessoa(nome, documento, idade, sexo);

        // Assert - Verificar resultado
        Assert.Equal(nome, pessoa.Nome);
        Assert.Equal(documento, pessoa.Documento);
        Assert.Equal(idade, pessoa.Idade);
        Assert.Equal(sexo, pessoa.Sexo);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ExemploTeste_Parametrizado(string nomeInvalido)
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => 
            new Pessoa(nomeInvalido, "12345678901", 30, Pessoa.SexoPessoa.Masculino));
    }

    [Fact]
    public void ExemploTeste_Validacao_ComMultiplasAssercoes()
    {
        // Arrange
        var pessoa = new Pessoa("João Silva", "12345678901", 30, Pessoa.SexoPessoa.Masculino);
        var vacinaId = Guid.NewGuid();
        var vacinacao = new Vacinacao(pessoa.Id, vacinaId, 1, DateTime.Today);

        // Act
        pessoa.RegistrarVacinacao(vacinacao);

        // Assert
        Assert.Single(pessoa.Vacinacoes);
        Assert.Contains(vacinacao, pessoa.Vacinacoes);
        Assert.Equal(pessoa.Id, vacinacao.PessoaId);
        Assert.Equal(vacinaId, vacinacao.VacinaId);
    }

    [Fact]
    public void ExemploTeste_Validator()
    {
        // Arrange
        var validator = new CartaoVacina.Application.Validators.CriarPessoaDtoValidator();
        var dto = new CartaoVacina.Application.DTOs.CriarPessoaDto
        {
            Nome = "", // Nome inválido
            Documento = "12345678901",
            Idade = 30,
            Sexo = Pessoa.SexoPessoa.Masculino
        };

        // Act
        var result = validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => 
            e.PropertyName == nameof(dto.Nome) && 
            e.ErrorMessage == "Nome é obrigatório");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(100)]
    [InlineData(150)]
    public void ExemploTeste_Teoria_ComVariosValores(int idadeValida)
    {
        // Arrange & Act
        var pessoa = new Pessoa("João Silva", "12345678901", idadeValida, Pessoa.SexoPessoa.Masculino);

        // Assert
        Assert.Equal(idadeValida, pessoa.Idade);
    }
}
