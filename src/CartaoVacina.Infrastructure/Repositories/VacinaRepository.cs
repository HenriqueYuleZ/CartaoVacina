using CartaoVacina.Application.Interfaces;
using CartaoVacina.Domain.Entities;
using CartaoVacina.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CartaoVacina.Infrastructure.Repositories;

public class VacinaRepository : BaseRepository<Vacina>, IVacinaRepository
{
    public VacinaRepository(CartaoVacinaDbContext context) : base(context) { }

    public async Task<Vacina?> GetByNomeAsync(string nome)
    {
        return await _context.Vacinas
            .FirstOrDefaultAsync(v => v.Nome.ToLower() == nome.ToLower());
    }

    public override async Task<List<Vacina>> GetAllAsync()
    {
        return await _context.Vacinas
            .Include(v => v.Vacinacoes)
            .ThenInclude(vacinacao => vacinacao.Pessoa)
            .ToListAsync();
    }

    public void Delete(Guid id)
    {
        var vacina = _context.Vacinas.Find(id);
        if (vacina != null)
        {
            _context.Vacinas.Remove(vacina);
        }
    }
}
