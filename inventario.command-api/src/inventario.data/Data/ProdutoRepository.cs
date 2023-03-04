using Dapper;
using inventario.business.Abstractions.Data;
using inventario.business.Models;
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
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AdicionarAsync(ProdutoModel produto)
        {
            await _unitOfWork.Connection
                .ExecuteAsync(sql: ProdutoStatements.InserirProduto,
                              param: ProdutoStatements.ObterParametros(produto),
                              transaction: _unitOfWork.Transaction);
        }

        public async Task AlterarAsync(ProdutoModel produto)
        {
            await _unitOfWork.Connection
                .ExecuteAsync(sql: ProdutoStatements.AlterarProduto,
                              param: ProdutoStatements.ObterParametros(produto),
                              transaction: _unitOfWork.Transaction);
        }

        public async Task<IEnumerable<ProdutoModel>> ListarAsync(Guid? id = null, string nome = null, string categoria = null, string descricao = null, bool? ativo = null)
        {
            return await _unitOfWork.Connection
                    .QueryAsync<ProdutoModel,CategoriaModel, ProdutoModel>(sql: ProdutoStatements.ListarProdutos,
                                              map: (produto, categoria) => { 
                                                  produto.Categoria = categoria; 
                                                  return produto; 
                                              },
                                              param: ProdutoStatements.ObterParametrosParaListar(id, nome, descricao, categoria, ativo), 
                                              transaction: _unitOfWork.Transaction,
                                              splitOn: $"{nameof(ProdutoModel.IdCategoria)}");
        }
    }
}
