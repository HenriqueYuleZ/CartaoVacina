using CartaoVacina.Domain.Entities;

namespace CartaoVacina.UnitTests.Domain.Entities;

public class BaseEntityTests
{
    private class TestEntity : BaseEntity
    {
        public void UpdateEntity()
        {
            SetUpdatedAt();
        }
    }

    [Fact]
    public void BaseEntity_DeveGerarIdAutomaticamente_QuandoCriada()
    {
        // Arrange & Act
        var entity = new TestEntity();

        // Assert
        Assert.NotEqual(Guid.Empty, entity.Id);
    }

    [Fact]
    public void BaseEntity_DeveDefinirCreatedAtAutomaticamente_QuandoCriada()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);
        
        // Act
        var entity = new TestEntity();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.True(entity.CreatedAt >= beforeCreation && entity.CreatedAt <= afterCreation);
    }

    [Fact]
    public void BaseEntity_DeveIniciarUpdatedAtComoNull_QuandoCriada()
    {
        // Arrange & Act
        var entity = new TestEntity();

        // Assert
        Assert.Null(entity.UpdatedAt);
    }

    [Fact]
    public void SetUpdatedAt_DeveDefinirUpdatedAt_QuandoChamado()
    {
        // Arrange
        var entity = new TestEntity();
        var beforeUpdate = DateTime.UtcNow.AddSeconds(-1);

        // Act
        entity.UpdateEntity();
        var afterUpdate = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.NotNull(entity.UpdatedAt);
        Assert.True(entity.UpdatedAt >= beforeUpdate && entity.UpdatedAt <= afterUpdate);
    }

    [Fact]
    public void BaseEntity_DeveGerarIdsUnicos_ParaDiferentesInstancias()
    {
        // Arrange & Act
        var entity1 = new TestEntity();
        var entity2 = new TestEntity();

        // Assert
        Assert.NotEqual(entity1.Id, entity2.Id);
    }
}
