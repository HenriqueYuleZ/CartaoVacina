# CartaoVacina - Testes Unitários

Este projeto contém testes unitários para o sistema CartaoVacina, um sistema de gerenciamento de cartão de vacinação desenvolvido em .NET 8.

## Estrutura dos Testes

O projeto de testes está organizado da seguinte forma:

```
tests/CartaoVacina.UnitTests/
├── Domain/
│   └── Entities/
│       ├── BaseEntityTests.cs
│       ├── PessoaTests.cs
│       ├── VacinaTests.cs
│       └── VacinacaoTests.cs
├── Application/
│   ├── DTOs/
│   │   ├── PessoaDtoTests.cs
│   │   ├── VacinaDtoTests.cs
│   │   └── VacinacaoDtoTests.cs
│   └── Validators/
│       ├── CriarPessoaDtoValidatorTests.cs
│       ├── CriarVacinaDtoValidatorTests.cs
│       └── CriarVacinacaoDtoValidatorTests.cs
└── GlobalUsings.cs
```

## Tecnologias Utilizadas

- **xUnit**: Framework de testes principal
- **FluentValidation**: Para testes dos validators
- **Moq**: Framework de mock para testes de unidade
- **.NET 8**: Plataforma de desenvolvimento

## Cobertura dos Testes

### Domain - Entidades
- **BaseEntity**: Testa criação de ID, CreatedAt, UpdatedAt
- **Pessoa**: Testa validações de nome, documento, idade, sexo e registro de vacinações
- **Vacina**: Testa validações de nome e criação da entidade
- **Vacinacao**: Testa validações de dose, data de aplicação e relacionamentos

### Application - DTOs
- **PessoaDto e CriarPessoaDto**: Testa instanciação e propriedades
- **VacinaDto e CriarVacinaDto**: Testa instanciação e propriedades
- **VacinacaoDto e CriarVacinacaoDto**: Testa instanciação e propriedades

### Application - Validators
- **CriarPessoaDtoValidator**: Testa validações de nome, documento, idade e sexo
- **CriarVacinaDtoValidator**: Testa validações de nome da vacina
- **CriarVacinacaoDtoValidator**: Testa validações de IDs, dose e data de aplicação

## Executando os Testes

### Todos os testes
```bash
dotnet test tests/CartaoVacina.UnitTests/CartaoVacina.UnitTests.csproj
```

### Com mais detalhes
```bash
dotnet test tests/CartaoVacina.UnitTests/CartaoVacina.UnitTests.csproj --verbosity normal
```

### Apenas um arquivo de teste específico
```bash
dotnet test tests/CartaoVacina.UnitTests/CartaoVacina.UnitTests.csproj --filter ClassName=PessoaTests
```

### Apenas um teste específico
```bash
dotnet test tests/CartaoVacina.UnitTests/CartaoVacina.UnitTests.csproj --filter "FullyQualifiedName~Pessoa_DeveSerCriadaComSucesso"
```

### Relatório de cobertura
```bash
dotnet test tests/CartaoVacina.UnitTests/CartaoVacina.UnitTests.csproj --collect:"XPlat Code Coverage"
```

## Padrões de Teste

### Nomenclatura
Os testes seguem o padrão **AAA (Arrange, Act, Assert)**:

```csharp
[Fact]
public void Entidade_DeveComportamento_QuandoCondicao()
{
    // Arrange - Preparar dados de entrada
    var entrada = new MinhaEntrada();
    
    // Act - Executar ação sendo testada
    var resultado = metodoSendoTestado(entrada);
    
    // Assert - Verificar resultado
    Assert.Equal(valorEsperado, resultado);
}
```

### Tipos de Teste
- **[Fact]**: Teste simples sem parâmetros
- **[Theory]**: Teste parametrizado com [InlineData]

### Validações Comuns
- Validação de entradas nulas/vazias
- Validação de tamanhos mínimos e máximos
- Validação de formatos (ex: apenas números para documento)
- Validação de regras de negócio

## Executando com Docker

Se você estiver usando Docker para o projeto, pode executar os testes dentro do container:

```bash
docker-compose exec api dotnet test tests/CartaoVacina.UnitTests/CartaoVacina.UnitTests.csproj
```

## Integração Contínua

Os testes estão configurados para serem executados automaticamente em pipelines de CI/CD. Certifique-se de que todos os testes estejam passando antes de fazer merge de suas alterações.

## Contribuindo

Ao adicionar novos testes:

1. Mantenha o padrão de nomenclatura
2. Use o padrão AAA
3. Teste casos de sucesso e falha
4. Adicione testes para validações de entrada
5. Mantenha os testes simples e focados
6. Execute todos os testes antes de commitar

## Estatísticas

- **Total de Testes**: 124
- **Taxa de Sucesso**: 100%
- **Cobertura**: Entidades de domínio, DTOs e Validators principais
