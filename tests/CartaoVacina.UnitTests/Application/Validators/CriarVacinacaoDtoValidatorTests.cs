using CartaoVacina.Application.DTOs;
using CartaoVacina.Application.Validators;
using FluentValidation;

namespace CartaoVacina.UnitTests.Application.Validators;

public class CriarVacinacaoDtoValidatorTests
{
    private readonly CriarVacinacaoDtoValidator _validator;

    public CriarVacinacaoDtoValidatorTests()
    {
        _validator = new CriarVacinacaoDtoValidator();
    }

    [Fact]
    public void Validator_DevePassar_QuandoDadosValidos()
    {
        // Arrange
        var dto = new CriarVacinacaoDto
        {
            PessoaId = Guid.NewGuid(),
            VacinaId = Guid.NewGuid(),
            Dose = 1,
            DataAplicacao = DateTime.Today
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Validator_DeveFalhar_QuandoPessoaIdVazio()
    {
        // Arrange
        var dto = new CriarVacinacaoDto
        {
            PessoaId = Guid.Empty,
            VacinaId = Guid.NewGuid(),
            Dose = 1,
            DataAplicacao = DateTime.Today
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.PessoaId) && 
            e.ErrorMessage == "PessoaId é obrigatório");
    }

    [Fact]
    public void Validator_DeveFalhar_QuandoVacinaIdVazio()
    {
        // Arrange
        var dto = new CriarVacinacaoDto
        {
            PessoaId = Guid.NewGuid(),
            VacinaId = Guid.Empty,
            Dose = 1,
            DataAplicacao = DateTime.Today
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.VacinaId) && 
            e.ErrorMessage == "VacinaId é obrigatório");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-5)]
    public void Validator_DeveFalhar_QuandoDoseMenorOuIgualZero(int doseInvalida)
    {
        // Arrange
        var dto = new CriarVacinacaoDto
        {
            PessoaId = Guid.NewGuid(),
            VacinaId = Guid.NewGuid(),
            Dose = doseInvalida,
            DataAplicacao = DateTime.Today
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Dose) && 
            e.ErrorMessage == "Dose deve ser maior que zero");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(10)]
    public void Validator_DevePassar_QuandoDoseValida(int doseValida)
    {
        // Arrange
        var dto = new CriarVacinacaoDto
        {
            PessoaId = Guid.NewGuid(),
            VacinaId = Guid.NewGuid(),
            Dose = doseValida,
            DataAplicacao = DateTime.Today
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validator_DeveFalhar_QuandoDataAplicacaoNoFuturo()
    {
        // Arrange
        var dto = new CriarVacinacaoDto
        {
            PessoaId = Guid.NewGuid(),
            VacinaId = Guid.NewGuid(),
            Dose = 1,
            DataAplicacao = DateTime.Today.AddDays(1)
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.DataAplicacao) && 
            e.ErrorMessage == "Data de aplicação não pode ser no futuro");
    }

    [Fact]
    public void Validator_DevePassar_QuandoDataAplicacaoHoje()
    {
        // Arrange
        var dataHoje = DateTime.Today;
        var dto = new CriarVacinacaoDto
        {
            PessoaId = Guid.NewGuid(),
            VacinaId = Guid.NewGuid(),
            Dose = 1,
            DataAplicacao = dataHoje
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validator_DevePassar_QuandoDataAplicacaoNoPassado()
    {
        // Arrange
        var dto = new CriarVacinacaoDto
        {
            PessoaId = Guid.NewGuid(),
            VacinaId = Guid.NewGuid(),
            Dose = 1,
            DataAplicacao = DateTime.Today.AddDays(-10)
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.True(result.IsValid);
    }
}
