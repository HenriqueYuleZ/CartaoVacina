using CartaoVacina.Application.DTOs;
using CartaoVacina.Application.Validators;
using FluentValidation;
using static CartaoVacina.Domain.Entities.Pessoa;

namespace CartaoVacina.UnitTests.Application.Validators;

public class CriarPessoaDtoValidatorTests
{
    private readonly CriarPessoaDtoValidator _validator;

    public CriarPessoaDtoValidatorTests()
    {
        _validator = new CriarPessoaDtoValidator();
    }

    [Fact]
    public void Validator_DevePassar_QuandoDadosValidos()
    {
        // Arrange
        var dto = new CriarPessoaDto
        {
            Nome = "João Silva",
            Documento = "12345678901",
            Idade = 30,
            Sexo = SexoPessoa.Masculino
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
        var dto = new CriarPessoaDto
        {
            Nome = nomeInvalido,
            Documento = "12345678901",
            Idade = 30,
            Sexo = SexoPessoa.Masculino
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
        var dto = new CriarPessoaDto
        {
            Nome = nomeInvalido,
            Documento = "12345678901",
            Idade = 30,
            Sexo = SexoPessoa.Masculino
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Nome) && 
            e.ErrorMessage == "Nome deve ter entre 2 e 200 caracteres");
    }

    [Fact]
    public void Validator_DeveFalhar_QuandoNomeMuitoLongo()
    {
        // Arrange
        var nomeLongo = new string('A', 201); // 201 caracteres
        var dto = new CriarPessoaDto
        {
            Nome = nomeLongo,
            Documento = "12345678901",
            Idade = 30,
            Sexo = SexoPessoa.Masculino
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Nome) && 
            e.ErrorMessage == "Nome deve ter entre 2 e 200 caracteres");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Validator_DeveFalhar_QuandoDocumentoVazio(string documentoInvalido)
    {
        // Arrange
        var dto = new CriarPessoaDto
        {
            Nome = "João Silva",
            Documento = documentoInvalido,
            Idade = 30,
            Sexo = SexoPessoa.Masculino
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Documento) && 
            e.ErrorMessage == "Documento é obrigatório");
    }

    [Theory]
    [InlineData("1234567890")] // Muito curto
    [InlineData("123456789012345")] // Muito longo
    public void Validator_DeveFalhar_QuandoDocumentoTamanhoInvalido(string documentoInvalido)
    {
        // Arrange
        var dto = new CriarPessoaDto
        {
            Nome = "João Silva",
            Documento = documentoInvalido,
            Idade = 30,
            Sexo = SexoPessoa.Masculino
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Documento) && 
            e.ErrorMessage == "Documento deve ter entre 11 e 14 caracteres");
    }

    [Theory]
    [InlineData("123456789AB")]
    [InlineData("12345678901A")]
    [InlineData("ABCDEFGHIJK")]
    public void Validator_DeveFalhar_QuandoDocumentoContemLetras(string documentoInvalido)
    {
        // Arrange
        var dto = new CriarPessoaDto
        {
            Nome = "João Silva",
            Documento = documentoInvalido,
            Idade = 30,
            Sexo = SexoPessoa.Masculino
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Documento) && 
            e.ErrorMessage == "Documento deve conter apenas números");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public void Validator_DeveFalhar_QuandoIdadeMenorOuIgualZero(int idadeInvalida)
    {
        // Arrange
        var dto = new CriarPessoaDto
        {
            Nome = "João Silva",
            Documento = "12345678901",
            Idade = idadeInvalida,
            Sexo = SexoPessoa.Masculino
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Idade) && 
            e.ErrorMessage == "Idade deve ser maior que zero");
    }

    [Fact]
    public void Validator_DeveFalhar_QuandoIdadeMaiorQue150()
    {
        // Arrange
        var dto = new CriarPessoaDto
        {
            Nome = "João Silva",
            Documento = "12345678901",
            Idade = 151,
            Sexo = SexoPessoa.Masculino
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Idade) && 
            e.ErrorMessage == "Idade deve ser menor ou igual a 150 anos");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(100)]
    [InlineData(150)]
    public void Validator_DevePassar_QuandoIdadeValida(int idadeValida)
    {
        // Arrange
        var dto = new CriarPessoaDto
        {
            Nome = "João Silva",
            Documento = "12345678901",
            Idade = idadeValida,
            Sexo = SexoPessoa.Masculino
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(SexoPessoa.Masculino)]
    [InlineData(SexoPessoa.Feminino)]
    [InlineData(SexoPessoa.Outro)]
    public void Validator_DevePassar_QuandoSexoValido(SexoPessoa sexoValido)
    {
        // Arrange
        var dto = new CriarPessoaDto
        {
            Nome = "João Silva",
            Documento = "12345678901",
            Idade = 30,
            Sexo = sexoValido
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("12345678901")] // CPF
    [InlineData("12345678901234")] // CNPJ
    public void Validator_DevePassar_QuandoDocumentoTamanhoValido(string documentoValido)
    {
        // Arrange
        var dto = new CriarPessoaDto
        {
            Nome = "João Silva",
            Documento = documentoValido,
            Idade = 30,
            Sexo = SexoPessoa.Masculino
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.True(result.IsValid);
    }
}
