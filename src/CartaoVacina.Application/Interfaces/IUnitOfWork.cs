
namespace CartaoVacina.Application.Interfaces;

public interface IUnitOfWork
{
    IPessoaRepository Pessoas { get; }
    IVacinaRepository Vacinas { get; }
    IVacinacaoRepository Vacinacao { get; }
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}