using inventario.business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventario.business.Abstractions.Data
{
    public interface IProdutoRepository
    {
        Task AdicionarAsync(ProdutoModel produto);

        Task AlterarAsync(ProdutoModel produto);

        Task<IReadOnlyList<ProdutoModel>> ListarAsync(string categoria, string descricao, bool ativo);
    }
}
