using inventario.business.Models.Request;
using inventario.business.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventario.business.Service
{
    public interface IProdutoService
    {
        Task<ProdutoResponse> AdicionarAsync(ProdutoRequest request);

        Task<ProdutoResponse> AlterarAsync(ProdutoRequest request);

        Task<IEnumerable<ProdutoResponse>> ListarAsync(string nome, string descricao, string categoria, bool ativo);
    }
}
