using CartaoVacina.Application.Interfaces;
using CartaoVacina.Domain.Exceptions;
using MediatR;

namespace CartaoVacina.Application.Commands.Pessoas;

public class DeletePessoaCommandHandler : IRequestHandler<DeletePessoaCommand, Unit>
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePessoaCommandHandler(IPessoaRepository pessoaRepository, IUnitOfWork unitOfWork)
    {
        _pessoaRepository = pessoaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeletePessoaCommand request, CancellationToken cancellationToken)
    {
        var pessoa = await _pessoaRepository.GetByIdAsync(request.Id);
        if (pessoa == null)
        {
            throw new NotFoundException(request.Id, "Pessoa");
        }

        _pessoaRepository.Delete(pessoa.Id);
        await _unitOfWork.CommitTransactionAsync();

        return Unit.Value;
    }
}