using inventario.business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventario.business.Abstractions.Data
{
    public interface IProdutoRepository
    {
        Task AdicionarAsync(ProdutoModel produto);

        Task AlterarAsync(ProdutoModel produto);

        Task<IEnumerable<ProdutoModel>> ListarAsync(Guid? id = null, string nome = null, string categoria = null, string descricao = null , bool? ativo = null);
    }
}
