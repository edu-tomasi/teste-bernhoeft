using inventario.business.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace inventario.business.Abstractions.Data
{
    public interface ICategoriaRepository
    {
        Task AdicionarAsync(CategoriaModel categoria, CancellationToken cancellationToken = default);

        Task AlterarAsync(CategoriaModel categoria, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<CategoriaModel>> ListarAsync(string nome, bool ativo, CancellationToken cancelationToken = default);
    }
}
