using CartaoVacina.Application.Interfaces;
using CartaoVacina.Domain.Entities;
using CartaoVacina.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CartaoVacina.Infrastructure.Repositories;

public class VacinacaoRepository : BaseRepository<Vacinacao>, IVacinacaoRepository
{
    public VacinacaoRepository(CartaoVacinaDbContext context) : base(context) { }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public override async Task<List<Vacinacao>> GetAllAsync()
    {
        return await _context.Vacinacoes
            .Include(v => v.Pessoa)
            .Include(v => v.Vacina)
            .ToListAsync();
    }

    public override async Task<Vacinacao?> GetByIdAsync(Guid id)
    {
        return await _context.Vacinacoes
            .Include(v => v.Pessoa)
            .Include(v => v.Vacina)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public Task<Vacinacao?> GetByPessoaIdAsync(Guid pessoaId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Vacinacao>> GetAllByPessoaIdAsync(Guid pessoaId)
    {
        return await _context.Vacinacoes
            .Include(v => v.Pessoa)
            .Include(v => v.Vacina)
            .Where(v => v.PessoaId == pessoaId)
            .OrderBy(v => v.DataAplicacao)
            .ToListAsync();
    }

    public Task<Vacinacao?> GetByVacinaIdAsync(Guid vacinaId)
    {
        throw new NotImplementedException();
    }
}