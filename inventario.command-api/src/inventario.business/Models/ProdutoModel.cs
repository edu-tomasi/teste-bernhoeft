using System;

namespace inventario.business.Models
{
    public record ProdutoModel
    {
        public Guid Id { get; init; }

        public string Nome { get; init; }

        public string Descricao { get; init; }

        public decimal Preco { get; init; }

        public Guid IdCategoria { get; init; }

    }
}
