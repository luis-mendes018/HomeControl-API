using Application.DTOs.Categoria;
using FluentValidation;

namespace Application.Validators.Categorias;

public class CategoriaUpdateDtoValidator : AbstractValidator<CategoriaUpdateDto>
{
    public CategoriaUpdateDtoValidator()
    {
        RuleFor(c => c.Descricao)
           .NotEmpty().WithMessage("A descrição é obrigatória.")
           .MaximumLength(200).WithMessage("A descrição deve ter no máximo 200 caracteres.");

    }
}
