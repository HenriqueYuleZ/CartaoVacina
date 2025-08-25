using CartaoVacina.Application.DTOs;
using CartaoVacina.Application.Validators;
using FluentValidation;

namespace CartaoVacina.UnitTests.Application.Validators;

public class CriarVacinaDtoValidatorTests
{
    private readonly CriarVacinaDtoValidator _validator;

    public CriarVacinaDtoValidatorTests()
    {
        _validator = new CriarVacinaDtoValidator();
    }

    [Fact]
    public void Validator_DevePassar_QuandoDadosValidos()
    {
        // Arrange
        var dto = new CriarVacinaDto
        {
            Nome = "Pfizer COVID-19"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Validator_DeveFalhar_QuandoNomeVazio(string nomeInvalido)
    {
        // Arrange
        var dto = new CriarVacinaDto
        {
            Nome = nomeInvalido
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Nome) && 
            e.ErrorMessage == "Nome é obrigatório");
    }

    [Theory]
    [InlineData("A")] // Muito curto
    public void Validator_DeveFalhar_QuandoNomeTamanhoInvalido(string nomeInvalido)
    {
        // Arrange
        var dto = new CriarVacinaDto
        {
            Nome = nomeInvalido
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Nome) && 
            e.ErrorMessage == "Nome deve ter entre 2 e 100 caracteres");
    }

    [Fact]
    public void Validator_DeveFalhar_QuandoNomeMuitoLongo()
    {
        // Arrange
        var nomeLongo = new string('A', 101); // 101 caracteres
        var dto = new CriarVacinaDto
        {
            Nome = nomeLongo
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Nome) && 
            e.ErrorMessage == "Nome deve ter entre 2 e 100 caracteres");
    }

    [Theory]
    [InlineData("Pfizer COVID-19")]
    [InlineData("AstraZeneca")]
    [InlineData("CoronaVac")]
    [InlineData("Janssen")]
    [InlineData("Moderna")]
    public void Validator_DevePassar_QuandoNomeValido(string nomeValido)
    {
        // Arrange
        var dto = new CriarVacinaDto
        {
            Nome = nomeValido
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("AB")] // Mínimo válido
    [InlineData("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")] // Máximo válido
    public void Validator_DevePassar_QuandoNomeTamanhoLimite(string nomeValido)
    {
        // Arrange
        var dto = new CriarVacinaDto
        {
            Nome = nomeValido
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.True(result.IsValid);
    }
}
