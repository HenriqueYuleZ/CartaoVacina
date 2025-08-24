using CartaoVacina.Domain.Entities;

namespace CartaoVacina.Application.Interfaces;

public interface IPessoaRepository
{
    Task<Pessoa?> GetByIdAsync(Guid id);
    Task<Pessoa?> GetByDocumentNumberAsync(string documentNumber);
    Task<List<Pessoa>> GetAllAsync();
    Task AddAsync(Pessoa pessoa);
    void Update(Pessoa pessoa);
    void Delete(Guid id);
    Task<bool> DocumentNumberExistsAsync(string documentNumber);
}