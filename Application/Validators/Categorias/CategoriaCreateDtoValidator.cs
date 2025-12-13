using Application.DTOs.Categoria;

using FluentValidation;

namespace Application.Validators.Categorias;

public class CategoriaCreateDtoValidator : AbstractValidator<CategoriaCreateDto>
{
    public CategoriaCreateDtoValidator()
    {
        RuleFor(c => c.Descricao)
            .NotEmpty().WithMessage("A descrição é obrigatória.")
            .MaximumLength(200).WithMessage("A descrição deve ter no máximo 200 caracteres.");
         
        RuleFor(c => c.Finalidade)
            .IsInEnum().WithMessage("A finalidade da categoria é inválida.");
    }
}
