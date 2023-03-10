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
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaRepository(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task AdicionarAsync(CategoriaModel categoria)
        {
            await _unitOfWork.Connection
                .ExecuteAsync(sql: CategoriaStatements.InserirCategoria,
                              param: CategoriaStatements.ObterParametros(categoria),
                              transaction: _unitOfWork.Transaction);
        }

        public async Task AlterarAsync(CategoriaModel categoria)
        {
            await _unitOfWork.Connection
                .ExecuteAsync(sql: CategoriaStatements.AlterarCategoria,
                              param: CategoriaStatements.ObterParametros(categoria),
                              transaction: _unitOfWork.Transaction);
        }

        public async Task<IEnumerable<CategoriaModel>> ListarAsync(FilterCategoriaRequest request)
        {
            return await _unitOfWork.Connection
                            .QueryAsync<CategoriaModel>(sql: CategoriaStatements.ListarCategorias,
                                                       param: CategoriaStatements.ObterParametrosParaListar(request),
                                                       transaction: _unitOfWork.Transaction);
        }

        public async Task RemoverAsync(Guid id)
        {
            await _unitOfWork.Connection
                    .ExecuteAsync(sql: CategoriaStatements.RemoverCategoria,
                                  param: new { Id = id },
                                  transaction: _unitOfWork.Transaction);
        }

    }
}
