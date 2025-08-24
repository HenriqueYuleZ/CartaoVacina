using CartaoVacina.Domain.Entities;

namespace CartaoVacina.Application.Interfaces;

public interface IVacinaRepository
{
    Task<Vacina?> GetByIdAsync(Guid id);
    Task<Vacina?> GetByNomeAsync(string nome);
    Task<List<Vacina>> GetAllAsync();
    Task<Vacina> AddAsync(Vacina vacina);
    void Update(Vacina vacina);
    void Delete(Guid id);
}