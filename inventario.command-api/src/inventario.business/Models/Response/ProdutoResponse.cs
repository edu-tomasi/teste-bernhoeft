using System;

namespace inventario.business.Models.Response
{
    public record ProdutoResponse
    {
        public Guid Id { get; init; }

        public string Type { get; } = "produto";

        public string Nome { get; init; }

        public bool Ativo { get; init; }

        public decimal Preco { get; set; }

        public string Descricao { get; set; }

        public Guid IdCategoria { get; set; }
    }
}
