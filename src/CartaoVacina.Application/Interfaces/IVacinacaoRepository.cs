using CartaoVacina.Domain.Entities;

namespace CartaoVacina.Application.Interfaces;

public interface IVacinacaoRepository
{
    Task<Vacinacao?> GetByIdAsync(Guid id);
    Task<Vacinacao?> GetByPessoaIdAsync(Guid pessoaId);
    Task<Vacinacao?> GetByVacinaIdAsync(Guid vacinaId);
    Task<List<Vacinacao>> GetAllAsync();
    Task<Vacinacao> AddAsync(Vacinacao vacinacao);
    void Update(Vacinacao vacinacao);
    void Delete(Guid id);
}