using System;

namespace inventario.business.Models.Request
{
    public record CategoriaRequest
    {
        public Guid? Id { get; init; }

        public string Type { get; } = "categoria";

        public string Nome { get; init; }

        public bool Ativo { get; init; }
    }
}
