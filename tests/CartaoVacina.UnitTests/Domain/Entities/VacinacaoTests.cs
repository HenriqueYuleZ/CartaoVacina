using CartaoVacina.Domain.Entities;

namespace CartaoVacina.UnitTests.Domain.Entities;

public class VacinacaoTests
{
    [Fact]
    public void Vacinacao_DeveSerCriadaComSucesso_QuandoDadosValidos()
    {
        // Arrange
        var pessoaId = Guid.NewGuid();
        var vacinaId = Guid.NewGuid();
        var dose = 1;
        var dataAplicacao = DateTime.Today;

        // Act
        var vacinacao = new Vacinacao(pessoaId, vacinaId, dose, dataAplicacao);

        // Assert
        Assert.Equal(pessoaId, vacinacao.PessoaId);
        Assert.Equal(vacinaId, vacinacao.VacinaId);
        Assert.Equal(dose, vacinacao.Dose);
        Assert.Equal(dataAplicacao.Date, vacinacao.DataAplicacao);
        Assert.NotEqual(Guid.Empty, vacinacao.Id);
        Assert.True(vacinacao.CreatedAt <= DateTime.UtcNow);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-5)]
    public void Vacinacao_DeveLancarExcecao_QuandoDoseInvalida(int doseInvalida)
    {
        // Arrange
        var pessoaId = Guid.NewGuid();
        var vacinaId = Guid.NewGuid();
        var dataAplicacao = DateTime.Today;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            new Vacinacao(pessoaId, vacinaId, doseInvalida, dataAplicacao));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void Vacinacao_DeveAceitarDiferentesDoses(int dose)
    {
        // Arrange
        var pessoaId = Guid.NewGuid();
        var vacinaId = Guid.NewGuid();
        var dataAplicacao = DateTime.Today;

        // Act
        var vacinacao = new Vacinacao(pessoaId, vacinaId, dose, dataAplicacao);

        // Assert
        Assert.Equal(dose, vacinacao.Dose);
    }

    [Fact]
    public void Vacinacao_DeveDefinirDataApenasComData_SemHorario()
    {
        // Arrange
        var pessoaId = Guid.NewGuid();
        var vacinaId = Guid.NewGuid();
        var dose = 1;
        var dataComHorario = new DateTime(2023, 12, 15, 14, 30, 0);

        // Act
        var vacinacao = new Vacinacao(pessoaId, vacinaId, dose, dataComHorario);

        // Assert
        Assert.Equal(dataComHorario.Date, vacinacao.DataAplicacao);
        Assert.Equal(TimeSpan.Zero, vacinacao.DataAplicacao.TimeOfDay);
    }

    [Fact]
    public void Vacinacao_DevePermitirGuidVazio_ParaPessoaIdEVacinaId()
    {
        // Arrange
        var pessoaId = Guid.Empty;
        var vacinaId = Guid.Empty;
        var dose = 1;
        var dataAplicacao = DateTime.Today;

        // Act
        var vacinacao = new Vacinacao(pessoaId, vacinaId, dose, dataAplicacao);

        // Assert
        Assert.Equal(pessoaId, vacinacao.PessoaId);
        Assert.Equal(vacinaId, vacinacao.VacinaId);
    }
}
