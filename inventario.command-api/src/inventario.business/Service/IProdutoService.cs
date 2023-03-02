using inventario.business.Models.Request;
using System.Threading;
using System.Threading.Tasks;

namespace inventario.business.Service
{
    public interface IProdutoService
    {
        Task AdicionarAsync(CategoriaRequest request, CancellationToken cancellationToken = default);
    }
}
