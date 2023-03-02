using inventario.business.Models.Request;
using inventario.business.Models.Response;
using System.Threading;
using System.Threading.Tasks;

namespace inventario.business.Service
{
    public interface ICategoriaService
    {
        Task<CategoriaResponse> AdicionarAsync(CategoriaRequest request, CancellationToken cancellationToken = default);
    }
}
