using inventario.business.Abstractions.Data;
using inventario.business.Models;
using inventario.business.Models.Request;
using inventario.business.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventario.business.Service
{
    public class ProdutoService : IProdutoService
    {
        private IProdutoRepository _produtoRepository;
        private IUnitOfWork _uow;

        public ProdutoService(IProdutoRepository produtoRepository, IUnitOfWork uow)
        {
            _produtoRepository = produtoRepository;
            _uow = uow;
        }

        public async Task<ProdutoResponse> AlterarAsync(ProdutoRequest request)
        {
            ProdutoModel produtoModel = CreateFrom(request);

            await _produtoRepository.AlterarAsync(produtoModel);

            return CreateFrom(produtoModel);
        }

        public async Task<IEnumerable<ProdutoResponse>> ListarAsync(Guid? id = null, string nome = null, string descricao = null, string categoria = null, bool? ativo = null)
        {
            var result = await _produtoRepository.ListarAsync(id, nome, descricao, categoria, ativo);

            return result.Select(s => CreateFrom(s));
        }

        public async Task<ProdutoResponse> AdicionarAsync(ProdutoRequest request)
        {
            ProdutoModel produtoModel = CreateFrom(request);

            _uow.BeginTransaction();
            await _produtoRepository.AdicionarAsync(produtoModel);
            _uow.Commit();

            return CreateFrom(produtoModel);
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
            Preco = model.Preco,
            Categoria = new ()
            {
                Id = model.Categoria.Id,
                Ativo = model.Categoria.Ativo,
                Nome = model.Categoria.Nome
            }

        };
    }
}
