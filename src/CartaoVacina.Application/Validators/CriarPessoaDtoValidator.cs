using FluentValidation;
using CartaoVacina.Application.DTOs;

namespace CartaoVacina.Application.Validators;

public class CriarPessoaDtoValidator : AbstractValidator<CriarPessoaDto>
{
    public CriarPessoaDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .Length(2, 200).WithMessage("Nome deve ter entre 2 e 200 caracteres");

        RuleFor(x => x.Documento)
            .NotEmpty().WithMessage("Documento é obrigatório")
            .Length(11, 14).WithMessage("Documento deve ter entre 11 e 14 caracteres")
            .Matches(@"^\d+$").WithMessage("Documento deve conter apenas números");

        RuleFor(x => x.Idade)
            .GreaterThan(0).WithMessage("Idade deve ser maior que zero")
            .LessThanOrEqualTo(150).WithMessage("Idade deve ser menor ou igual a 150 anos");

        RuleFor(x => x.Sexo)
            .IsInEnum().WithMessage("Valor inválido para Sexo. Use: 0=Masculino, 1=Feminino, 2=Outro");
    }
}
