using FluentValidation;
using CartaoVacina.Application.DTOs;

namespace CartaoVacina.Application.Validators;

public class CriarVacinaDtoValidator : AbstractValidator<CriarVacinaDto>
{
    public CriarVacinaDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .Length(2, 100).WithMessage("Nome deve ter entre 2 e 100 caracteres");
    }
}
