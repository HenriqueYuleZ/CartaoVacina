using Microsoft.EntityFrameworkCore.Storage;
using CartaoVacina.Application.Interfaces;
using CartaoVacina.Infrastructure.Data.Contexts;

namespace CartaoVacina.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CartaoVacinaDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(CartaoVacinaDbContext context)
    {
        _context = context;
        Pessoas = new PessoaRepository(_context);
        Vacinas = new VacinaRepository(_context);
        Vacinacao = new VacinacaoRepository(_context);
    }

    public IPessoaRepository Pessoas { get; }
    public IVacinaRepository Vacinas { get; }
    public IVacinacaoRepository Vacinacao { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}