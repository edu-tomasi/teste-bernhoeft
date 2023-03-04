using System;
using System.Collections.Generic;

namespace inventario.business.Models
{
    public record CategoriaModel
    {
        public Guid Id { get; init; }

        public string Nome { get; init; }

        public bool Ativo { get; init; }

        public ICollection<ProdutoModel> Produtos { get; set; }
    }
}
