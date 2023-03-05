using FluentValidation;
using inventario.business.Models.Request;

namespace inventario.web_api.Validators
{
    public class ProdutoValidator : AbstractValidator<ProdutoRequest>
    {
        public ProdutoValidator()
        {
            RuleFor(p => p.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage("O nome do Produto não pode ser uma string vazia.")
                .MinimumLength(0)
                .MaximumLength(200)
                .WithMessage("O nome do Produto deve ter no máximo 200 caracteres.");

            RuleFor(p => p.Descricao)
                .MinimumLength(0)
                .MaximumLength(400)
                .WithMessage("A descrição do Produto deve ter no máximo 400 caracteres.");

            RuleFor(p => p.Preco)
                .NotNull()
                .WithMessage("O preço do Produto não pode ser nulo.");

            RuleFor(p => p.IdCategoria)
                .NotNull()
                .WithMessage("O identificador da categoria do Produto deve ser informado.");
        }
    }
}
