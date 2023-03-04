using inventario.business.Models.Request;
using inventario.business.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventario.business.Service
{
    public interface IProdutoService
    {
        Task<ProdutoResponse> AdicionarAsync(ProdutoRequest request);

        Task<ProdutoResponse> AlterarAsync(ProdutoRequest request);

        Task<IEnumerable<ProdutoResponse>> ListarAsync(Guid? Id = null, string Nome = null, string Descricao = null, string Categoria = null, bool? Ativo = null);
    }
}
