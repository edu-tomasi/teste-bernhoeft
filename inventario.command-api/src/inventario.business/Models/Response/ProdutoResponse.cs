using System;

namespace inventario.business.Models.Response
{
    public record ProdutoResponse
    {
        public Guid Id { get; init; }

        public string Nome { get; init; }

        public bool Ativo { get; init; }

        public decimal Preco { get; set; }

        public string Descricao { get; set; }

        public CategoriaResponse Categoria { get; set; }

    }
}
