using Application.DTOs.Transacao;
using FluentValidation;

namespace Application.Validators.Transacoes;

public class TransacaoCreateDtoValidator : AbstractValidator<TransacaoCreateDto>
{
    public TransacaoCreateDtoValidator()
    {
        RuleFor(t => t.Valor)
            .NotEmpty().WithMessage("O valor da transação é obrigatório.")
            .GreaterThan(0).WithMessage("O valor da transação deve ser maior que zero.");

        RuleFor(t => t.Descricao).NotEmpty()
            .WithMessage("A descrição é obrigatória.")
            .MaximumLength(200).WithMessage("A descrição não pode exceder 200 caracteres.");
    }
}
