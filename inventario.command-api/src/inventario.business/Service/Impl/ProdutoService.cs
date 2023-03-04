using inventario.business.Abstractions.Data;
using inventario.business.Models.Request;
using inventario.business.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventario.business.Service
{
    public class ProdutoService : IProdutoService
    {
        private IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository) 
            => _produtoRepository = produtoRepository;

        public Task<ProdutoResponse> AlterarAsync(CategoriaRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProdutoResponse>> ListarAsync(string nome, string descricao, string categoria, bool ativo)
        {
            throw new NotImplementedException();
        }

        Task<ProdutoResponse> IProdutoService.AdicionarAsync(CategoriaRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
