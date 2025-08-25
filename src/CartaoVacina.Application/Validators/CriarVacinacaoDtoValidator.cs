using FluentValidation;
using CartaoVacina.Application.DTOs;

namespace CartaoVacina.Application.Validators;

public class CriarVacinacaoDtoValidator : AbstractValidator<CriarVacinacaoDto>
{
    public CriarVacinacaoDtoValidator()
    {
        RuleFor(x => x.PessoaId)
            .NotEmpty().WithMessage("PessoaId é obrigatório");

        RuleFor(x => x.VacinaId)
            .NotEmpty().WithMessage("VacinaId é obrigatório");

        RuleFor(x => x.Dose)
            .GreaterThan(0).WithMessage("Dose deve ser maior que zero");

        RuleFor(x => x.DataAplicacao)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Data de aplicação não pode ser no futuro");
    }
}
