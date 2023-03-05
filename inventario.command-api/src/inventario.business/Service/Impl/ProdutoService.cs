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
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUnitOfWork _uow;

        public ProdutoService(IProdutoRepository produtoRepository, IUnitOfWork uow)
            => (_produtoRepository, _uow) = (produtoRepository, uow);

        public async Task<ProdutoResponse> AlterarAsync(Guid id, ProdutoRequest request)
        {
            ProdutoModel produtoModel = CreateFrom(request);

            try
            {
                _uow.BeginTransaction();

                if (id != request.Id)
                {
                    throw new InvalidOperationException("O Id informado no URL não é o mesmo do informado no corpo da requisição.");
                }

                var produtos = await _produtoRepository.ListarAsync(new() { Id = id });
                if (!produtos.Any())
                {
                    throw new InvalidOperationException("Não foi encontrado o produto para edição.");
                }

                await _produtoRepository.AlterarAsync(new()
                {
                    Id = id,
                    Ativo = produtoModel.Ativo,
                    Categoria = produtoModel.Categoria,
                    Descricao = produtoModel.Descricao,
                    IdCategoria = produtoModel.IdCategoria,
                    Nome = produtoModel.Nome,
                    Preco = produtoModel.Preco
                });

                produtoModel = produtos.First();

                _uow.Commit();
            }
            catch (Exception)
            {
                _uow.Rollback();
                throw;
            }

            return CreateFrom(produtoModel);
        }

        public async Task<IEnumerable<ProdutoResponse>> ListarAsync(FilterProdutoRequest request)
        {
            var result = await _produtoRepository.ListarAsync(request);

            return result.Select(s => CreateFrom(s));
        }

        public async Task<ProdutoResponse> AdicionarAsync(ProdutoRequest request)
        {
            ProdutoModel produtoModel = CreateFrom(request);

            try
            {
                _uow.BeginTransaction();

                await _produtoRepository.AdicionarAsync(produtoModel);

                _uow.Commit();
            }
            catch (Exception)
            {
                _uow.Rollback();
                throw;
            }
            finally
            {
                var produtos = await _produtoRepository.ListarAsync(new() { Id = produtoModel.Id });
                produtoModel = produtos.First();
            }

            return CreateFrom(produtoModel);
        }

        public async Task RemoverAsync(Guid id)
        {
            try
            {
                _uow.BeginTransaction();
                await _produtoRepository.RemoverAsync(id);
                _uow.Commit();
            }
            catch (Exception)
            {
                _uow.Rollback();
                throw;
            }

        }

        private static ProdutoModel CreateFrom(ProdutoRequest request) => new()
        {
            Id = request.Id,
            Ativo = request.Ativo,
            Nome = request.Nome,
            IdCategoria = request.IdCategoria,
            Preco = request.Preco,
            Descricao = request.Descricao
        };

        private static ProdutoResponse CreateFrom(ProdutoModel model) => new()
        {
            Id = model.Id,
            Ativo = model.Ativo,
            Nome = model.Nome,
            Descricao = model.Descricao,
            Preco = model.Preco,
            Categoria = new()
            {
                Id = model.Categoria.Id,
                Ativo = model.Categoria.Ativo,
                Nome = model.Categoria.Nome
            }

        };
    }
}
