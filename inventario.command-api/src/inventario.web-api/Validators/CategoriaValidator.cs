using FluentValidation;
using inventario.business.Models.Request;

namespace inventario.web_api.Validators
{
    public class CategoriaValidator : AbstractValidator<CategoriaRequest>
    {
        public CategoriaValidator()
        {
            RuleFor(c => c.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage("O nome da Categoria não pode ser uma string vazia.")
                .MinimumLength(0)
                .MaximumLength(200)
                .WithMessage("O nome da Categoria deve ter no máximo 200 caracteres.");
                
        }
    }
}
