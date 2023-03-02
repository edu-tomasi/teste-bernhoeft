using System;

namespace inventario.business.Models.Response
{
    public record ProdutoResponse
    {
        public Guid Id { get; init; }

        public string Type { get; } = "produto";

        public string Nome { get; init; }

        public string Ativo { get; init; }

        public CategoriaResponse Categoria { get; init; }
    }
}
