using System;

namespace inventario.business.Models.Response
{
    public record CategoriaResponse
    {
        public Guid Id { get; init; }

        public string Type { get; } = "categoria";

        public string Nome { get; init; }

        public bool Ativo { get; init; }
    }
}
