using CartaoVacina.Application.DTOs;

namespace CartaoVacina.UnitTests.Application.DTOs;

public class VacinacaoDtoTests
{
    [Fact]
    public void VacinacaoDto_DeveSerInstanciada_ComPropriedadesCorretas()
    {
        // Arrange
        var id = Guid.NewGuid();
        var pessoaId = Guid.NewGuid();
        var vacinaId = Guid.NewGuid();
        var dose = 1;
        var dataAplicacao = DateTime.Today;

        // Act
        var dto = new VacinacaoDto
        {
            Id = id,
            PessoaId = pessoaId,
            VacinaId = vacinaId,
            Dose = dose,
            DataAplicacao = dataAplicacao
        };

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(pessoaId, dto.PessoaId);
        Assert.Equal(vacinaId, dto.VacinaId);
        Assert.Equal(dose, dto.Dose);
        Assert.Equal(dataAplicacao, dto.DataAplicacao);
    }

    [Fact]
    public void CriarVacinacaoDto_DeveSerInstanciada_ComPropriedadesCorretas()
    {
        // Arrange
        var pessoaId = Guid.NewGuid();
        var vacinaId = Guid.NewGuid();
        var dose = 2;
        var dataAplicacao = DateTime.Today.AddDays(-7);

        // Act
        var dto = new CriarVacinacaoDto
        {
            PessoaId = pessoaId,
            VacinaId = vacinaId,
            Dose = dose,
            DataAplicacao = dataAplicacao
        };

        // Assert
        Assert.Equal(pessoaId, dto.PessoaId);
        Assert.Equal(vacinaId, dto.VacinaId);
        Assert.Equal(dose, dto.Dose);
        Assert.Equal(dataAplicacao, dto.DataAplicacao);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void VacinacaoDto_DeveAceitarDiferentesDoses(int dose)
    {
        // Arrange & Act
        var dto = new VacinacaoDto
        {
            Id = Guid.NewGuid(),
            PessoaId = Guid.NewGuid(),
            VacinaId = Guid.NewGuid(),
            Dose = dose,
            DataAplicacao = DateTime.Today
        };

        // Assert
        Assert.Equal(dose, dto.Dose);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void CriarVacinacaoDto_DeveAceitarDiferentesDoses(int dose)
    {
        // Arrange & Act
        var dto = new CriarVacinacaoDto
        {
            PessoaId = Guid.NewGuid(),
            VacinaId = Guid.NewGuid(),
            Dose = dose,
            DataAplicacao = DateTime.Today
        };

        // Assert
        Assert.Equal(dose, dto.Dose);
    }

    [Fact]
    public void VacinacaoDto_DevePermitirGuidsVazios()
    {
        // Arrange & Act
        var dto = new VacinacaoDto
        {
            Id = Guid.Empty,
            PessoaId = Guid.Empty,
            VacinaId = Guid.Empty,
            Dose = 1,
            DataAplicacao = DateTime.Today
        };

        // Assert
        Assert.Equal(Guid.Empty, dto.Id);
        Assert.Equal(Guid.Empty, dto.PessoaId);
        Assert.Equal(Guid.Empty, dto.VacinaId);
    }

    [Fact]
    public void CriarVacinacaoDto_DevePermitirGuidsVazios()
    {
        // Arrange & Act
        var dto = new CriarVacinacaoDto
        {
            PessoaId = Guid.Empty,
            VacinaId = Guid.Empty,
            Dose = 1,
            DataAplicacao = DateTime.Today
        };

        // Assert
        Assert.Equal(Guid.Empty, dto.PessoaId);
        Assert.Equal(Guid.Empty, dto.VacinaId);
    }
}
