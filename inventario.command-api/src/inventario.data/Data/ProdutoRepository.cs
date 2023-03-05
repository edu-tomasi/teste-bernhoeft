using Dapper;
using inventario.business.Abstractions.Data;
using inventario.business.Models;
using inventario.business.Models.Request;
using inventario.data.Data.Statements;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventario.data.Data
{
    public class ProdutoRepository : IProdutoRepository
    {
        private IUnitOfWork _unitOfWork;

        public ProdutoRepository(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task AdicionarAsync(ProdutoModel produto)
        {
            _ = await _unitOfWork.Connection
                .ExecuteAsync(sql: ProdutoStatements.InserirProduto,
                              param: ProdutoStatements.ObterParametros(produto),
                              transaction: _unitOfWork.Transaction);
        }

        public async Task AlterarAsync(ProdutoModel produto)
        {
            _ = await _unitOfWork.Connection
                .ExecuteAsync(sql: ProdutoStatements.AlterarProduto,
                              param: ProdutoStatements.ObterParametros(produto),
                              transaction: _unitOfWork.Transaction);
        }

        public async Task<IEnumerable<ProdutoModel>> ListarAsync(FilterProdutoRequest request)
        {
            return await _unitOfWork.Connection
                    .QueryAsync<ProdutoModel, CategoriaModel, ProdutoModel>(
                                              sql: ProdutoStatements.ListarProdutos,
                                              map: (produto, categoria) =>
                                              {
                                                  produto.Categoria = categoria;
                                                  return produto;
                                              },
                                              param: ProdutoStatements.ObterParametrosParaListar(request),
                                              transaction: _unitOfWork.Transaction,
                                              splitOn: $"{nameof(ProdutoModel.IdCategoria)}");
        }

        public async Task RemoverAsync(Guid id)
        {
            _ = await _unitOfWork.Connection
                    .ExecuteAsync(sql: ProdutoStatements.RemoverProduto,
                                  param: new { Id = id },
                                  transaction: _unitOfWork.Transaction);
        }
    }
}
