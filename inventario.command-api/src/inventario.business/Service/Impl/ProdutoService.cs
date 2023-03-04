using inventario.business.Abstractions.Data;
using inventario.business.Models;
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

        public async Task<ProdutoResponse> AlterarAsync(ProdutoRequest request)
        {
            ProdutoModel produtoModel = CreateFrom(request);

            await _produtoRepository.AlterarAsync(produtoModel);

            return CreateFrom(produtoModel);
        }

        public Task<IEnumerable<ProdutoResponse>> ListarAsync(string nome, string descricao, string categoria, bool ativo)
        {
            throw new NotImplementedException();
        }

        public async Task<ProdutoResponse> AdicionarAsync(ProdutoRequest request)
        {
            ProdutoModel produtoModel = CreateFrom(request);

            await _produtoRepository.AdicionarAsync(produtoModel);

            return CreateFrom(produtoModel); ;
        }

        private ProdutoModel CreateFrom(ProdutoRequest request) => new()
        {
            Id = request.Id,
            Ativo = request.Ativo,
            Nome = request.Nome,
            IdCategoria = request.IdCategoria,
            Preco = request.Preco,
            Descricao = request.Descricao
        };

        private ProdutoResponse CreateFrom(ProdutoModel model) => new()
        {
            Id = model.Id,
            Ativo = model.Ativo,
            Nome = model.Nome,
            Descricao = model.Descricao,
            IdCategoria = model.IdCategoria,
            Preco = model.Preco,
        };
    }
}
