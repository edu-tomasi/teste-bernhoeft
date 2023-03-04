using System;

namespace inventario.business.Models.Request
{
    public record ProdutoRequest
    {
        public Guid Id { get; } = Guid.NewGuid();

        public string Type { get; } = "produto";

        public string Nome { get; init; }

        public decimal Preco { get; init; }

        public bool Ativo { get; init; }

        public Guid IdCategoria { get; init; }

        public string Descricao { get; init; }
    }
}
