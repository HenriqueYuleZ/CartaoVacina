using CartaoVacina.Domain.Entities;
using static CartaoVacina.Domain.Entities.Pessoa;

namespace CartaoVacina.UnitTests.Domain.Entities;

public class PessoaTests
{
    [Fact]
    public void Pessoa_DeveSerCriadaComSucesso_QuandoDadosValidos()
    {
        // Arrange
        var nome = "João Silva";
        var documento = "12345678901";
        var idade = 30;
        var sexo = SexoPessoa.Masculino;

        // Act
        var pessoa = new Pessoa(nome, documento, idade, sexo);

        // Assert
        Assert.Equal(nome, pessoa.Nome);
        Assert.Equal(documento, pessoa.Documento);
        Assert.Equal(idade, pessoa.Idade);
        Assert.Equal(sexo, pessoa.Sexo);
        Assert.NotEqual(Guid.Empty, pessoa.Id);
        Assert.True(pessoa.CreatedAt <= DateTime.UtcNow);
        Assert.Empty(pessoa.Vacinacoes);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Pessoa_DeveLancarExcecao_QuandoNomeInvalido(string nomeInvalido)
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => 
            new Pessoa(nomeInvalido, "12345678901", 30, SexoPessoa.Masculino));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Pessoa_DeveLancarExcecao_QuandoDocumentoInvalido(string documentoInvalido)
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => 
            new Pessoa("João Silva", documentoInvalido, 30, SexoPessoa.Masculino));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public void Pessoa_DeveLancarExcecao_QuandoIdadeInvalida(int idadeInvalida)
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => 
            new Pessoa("João Silva", "12345678901", idadeInvalida, SexoPessoa.Masculino));
    }

    [Fact]
    public void Pessoa_DeveRemoverEspacosEmBranco_NoNomeEDocumento()
    {
        // Arrange
        var nome = "  João Silva  ";
        var documento = "  12345678901  ";

        // Act
        var pessoa = new Pessoa(nome, documento, 30, SexoPessoa.Masculino);

        // Assert
        Assert.Equal("João Silva", pessoa.Nome);
        Assert.Equal("12345678901", pessoa.Documento);
    }

    [Fact]
    public void RegistrarVacinacao_DeveAdicionarVacinacao_QuandoChamado()
    {
        // Arrange
        var pessoa = new Pessoa("João Silva", "12345678901", 30, SexoPessoa.Masculino);
        var vacinaId = Guid.NewGuid();
        var vacinacao = new Vacinacao(pessoa.Id, vacinaId, 1, DateTime.Today);

        // Act
        pessoa.RegistrarVacinacao(vacinacao);

        // Assert
        Assert.Single(pessoa.Vacinacoes);
        Assert.Contains(vacinacao, pessoa.Vacinacoes);
    }

    [Theory]
    [InlineData(SexoPessoa.Masculino)]
    [InlineData(SexoPessoa.Feminino)]
    [InlineData(SexoPessoa.Outro)]
    public void Pessoa_DeveAceitarTodosOsValoresDeSexo(SexoPessoa sexo)
    {
        // Arrange & Act
        var pessoa = new Pessoa("João Silva", "12345678901", 30, sexo);

        // Assert
        Assert.Equal(sexo, pessoa.Sexo);
    }
}
