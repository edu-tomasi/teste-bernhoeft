using System;

namespace inventario.business.Models.Request
{
    public record FilterCategoriaRequest
    {
        public Guid? Id { get; init; } = null;

        public string Nome { get; init; } = null;

        public bool? Ativo { get; init; } = null;
    }
}
