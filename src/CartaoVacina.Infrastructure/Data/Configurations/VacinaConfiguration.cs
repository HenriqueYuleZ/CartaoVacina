using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CartaoVacina.Domain.Entities;

namespace CartaoVacina.Infrastructure.Data.Configurations;

public class VacinaConfiguration : IEntityTypeConfiguration<Vacina>
{
    public void Configure(EntityTypeBuilder<Vacina> builder)
    {
        builder.ToTable("Vacinas");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .ValueGeneratedNever();

        builder.Property(v => v.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(v => v.CreatedAt)
            .IsRequired();

        builder.Property(v => v.UpdatedAt);

        // Relationships
        builder.HasMany(v => v.Vacinacoes)
            .WithOne(vac => vac.Vacina)
            .HasForeignKey(vac => vac.VacinaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
