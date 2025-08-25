using CartaoVacina.Application.DTOs;

namespace CartaoVacina.UnitTests.Application.DTOs;

public class VacinaDtoTests
{
    [Fact]
    public void VacinaDto_DeveSerInstanciada_ComPropriedadesCorretas()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Pfizer COVID-19";

        // Act
        var dto = new VacinaDto
        {
            Id = id,
            Nome = nome
        };

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(nome, dto.Nome);
    }

    [Fact]
    public void CriarVacinaDto_DeveSerInstanciada_ComPropriedadesCorretas()
    {
        // Arrange
        var nome = "AstraZeneca COVID-19";

        // Act
        var dto = new CriarVacinaDto
        {
            Nome = nome
        };

        // Assert
        Assert.Equal(nome, dto.Nome);
    }

    [Theory]
    [InlineData("Pfizer COVID-19")]
    [InlineData("AstraZeneca")]
    [InlineData("CoronaVac")]
    [InlineData("Janssen")]
    [InlineData("Moderna")]
    public void VacinaDto_DeveAceitarDiferentesNomes(string nome)
    {
        // Arrange & Act
        var dto = new VacinaDto
        {
            Id = Guid.NewGuid(),
            Nome = nome
        };

        // Assert
        Assert.Equal(nome, dto.Nome);
    }

    [Theory]
    [InlineData("Pfizer COVID-19")]
    [InlineData("AstraZeneca")]
    [InlineData("CoronaVac")]
    [InlineData("Janssen")]
    [InlineData("Moderna")]
    public void CriarVacinaDto_DeveAceitarDiferentesNomes(string nome)
    {
        // Arrange & Act
        var dto = new CriarVacinaDto
        {
            Nome = nome
        };

        // Assert
        Assert.Equal(nome, dto.Nome);
    }
}
