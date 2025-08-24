using Microsoft.EntityFrameworkCore;
using CartaoVacina.Application.Interfaces;
using CartaoVacina.Domain.Entities;
using CartaoVacina.Infrastructure.Data.Contexts;

namespace CartaoVacina.Infrastructure.Repositories;

public class PessoaRepository : BaseRepository<Pessoa>, IPessoaRepository
{
    public PessoaRepository(CartaoVacinaDbContext context) : base(context)
    {
    }

    public async Task<Pessoa?> GetByDocumentNumberAsync(string documentNumber)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.Documento == documentNumber);
    }

    public async Task<bool> DocumentoExisteAsync(string documentNumber)
    {
        var query = _dbSet.Where(p => p.Documento == documentNumber);

        return await query.AnyAsync();
    }

    public override async Task<Pessoa?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(p => p.Vacinacoes)
            .ThenInclude(v => v.Vacina)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async void Delete(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}