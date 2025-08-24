using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CartaoVacina.Domain.Entities;

namespace CartaoVacina.Infrastructure.Data.Configurations;

public class VacinacaoConfiguration : IEntityTypeConfiguration<Vacinacao>
{
    public void Configure(EntityTypeBuilder<Vacinacao> builder)
    {
        builder.ToTable("Vacinacoes");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .ValueGeneratedNever();

        builder.Property(v => v.Dose)
            .IsRequired();

        builder.Property(v => v.DataAplicacao)
            .IsRequired();

        builder.Property(v => v.CreatedAt)
            .IsRequired();

        builder.Property(v => v.UpdatedAt);

        // Foreign Keys
        builder.Property(v => v.PessoaId)
            .IsRequired();

        builder.Property(v => v.VacinaId)
            .IsRequired();

        // Relationships
        builder.HasOne(v => v.Pessoa)
            .WithMany(p => p.Vacinacoes)
            .HasForeignKey(v => v.PessoaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(v => v.Vacina)
            .WithMany(vac => vac.Vacinacoes)
            .HasForeignKey(v => v.VacinaId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(v => v.PessoaId)
            .HasDatabaseName("IX_Vacinacoes_PessoaId");

        builder.HasIndex(v => v.VacinaId)
            .HasDatabaseName("IX_Vacinacoes_VacinaId");
    }
}
