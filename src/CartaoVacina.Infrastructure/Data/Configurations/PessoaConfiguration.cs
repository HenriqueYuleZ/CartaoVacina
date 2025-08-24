using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CartaoVacina.Domain.Entities;

namespace CartaoVacina.Infrastructure.Data.Configurations;

public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.ToTable("Pessoas");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Documento)
            .IsRequired()
            .HasMaxLength(14);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt);

        // Index
        builder.HasIndex(p => p.Documento)
            .IsUnique()
            .HasDatabaseName("IX_Pessoas_Documento");

        // Relationships
        builder.HasMany(p => p.Vacinacoes)
            .WithOne(v => v.Pessoa)
            .HasForeignKey(v => v.PessoaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}