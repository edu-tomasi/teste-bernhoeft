using System;

namespace inventario.business.Models.Request
{
    public record FilterProdutoRequest
    {
        public Guid? Id { get; init; } = null;

        public string Nome { get; init; } = null;

        public Guid? IdCategoria { get; init; } = null;

        public string Descricao { get; init; } = null;

        public string Categoria { get; init; } = null;

        public bool? Ativo { get; init; } = null;
    }
}
