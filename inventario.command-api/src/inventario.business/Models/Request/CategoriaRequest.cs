using System;

namespace inventario.business.Models.Request
{
    public record CategoriaRequest
    {
        public Guid Id { get; } = Guid.NewGuid();

        public string Type { get; } = "categoria";

        public string Nome { get; init; }

        public bool Ativo { get; set; }
    }
}
