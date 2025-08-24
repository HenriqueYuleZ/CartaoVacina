using Microsoft.EntityFrameworkCore;
using CartaoVacina.Domain.Entities;

namespace CartaoVacina.Infrastructure.Data.Contexts;

public class CartaoVacinaDbContext : DbContext
{
    public CartaoVacinaDbContext(DbContextOptions<CartaoVacinaDbContext> options) : base(options)
    {
    }

    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Vacina> Vacinas { get; set; }
    public DbSet<Vacinacao> Vacinacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CartaoVacinaDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Update UpdatedAt for modified entities
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && e.State == EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            var entity = (BaseEntity)entityEntry.Entity;
            var property = entity.GetType().GetProperty("UpdatedAt");
            property?.SetValue(entity, DateTime.UtcNow);
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}