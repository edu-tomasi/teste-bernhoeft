using inventario.business.Models;
using inventario.business.Models.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventario.business.Abstractions.Data
{
    public interface IProdutoRepository
    {
        Task AdicionarAsync(ProdutoModel produto);

        Task AlterarAsync(ProdutoModel produto);

        Task<IEnumerable<ProdutoModel>> ListarAsync(FilterProdutoRequest request);

        Task RemoverAsync(Guid id);
    }
}
