using CartaoVacina.Domain.Entities;

namespace CartaoVacina.UnitTests.Domain.Entities;

public class VacinaTests
{
    [Fact]
    public void Vacina_DeveSerCriadaComSucesso_QuandoDadosValidos()
    {
        // Arrange
        var nome = "Pfizer COVID-19";

        // Act
        var vacina = new Vacina(nome);

        // Assert
        Assert.Equal(nome, vacina.Nome);
        Assert.NotEqual(Guid.Empty, vacina.Id);
        Assert.True(vacina.CreatedAt <= DateTime.UtcNow);
        Assert.Empty(vacina.Vacinacoes);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Vacina_DeveLancarExcecao_QuandoNomeInvalido(string nomeInvalido)
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => new Vacina(nomeInvalido));
    }

    [Fact]
    public void Vacina_DeveRemoverEspacosEmBranco_NoNome()
    {
        // Arrange
        var nome = "  Pfizer COVID-19  ";

        // Act
        var vacina = new Vacina(nome);

        // Assert
        Assert.Equal("Pfizer COVID-19", vacina.Nome);
    }

    [Theory]
    [InlineData("Pfizer COVID-19")]
    [InlineData("AstraZeneca")]
    [InlineData("CoronaVac")]
    [InlineData("Janssen")]
    public void Vacina_DeveAceitarDiferentesNomes(string nome)
    {
        // Arrange & Act
        var vacina = new Vacina(nome);

        // Assert
        Assert.Equal(nome, vacina.Nome);
    }
}
