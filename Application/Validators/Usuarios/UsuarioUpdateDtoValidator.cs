using Application.DTOs.Usuario;

using FluentValidation;

namespace Application.Validators.Usuarios;
public class UsuarioUpdateDtoValidator : AbstractValidator<UsuarioUpdateDto>
{
    public UsuarioUpdateDtoValidator()
    {
        RuleFor(u => u.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome não pode exceder 100 caracteres.");

        RuleFor(u => u.Idade).NotEmpty()
            .WithMessage("A idade é obrigatória.")
            .GreaterThan(0).WithMessage("A idade deve ser maior que zero.");
    }
}
