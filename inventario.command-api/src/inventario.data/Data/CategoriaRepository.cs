using Dapper;
using inventario.business.Abstractions.Data;
using inventario.business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using inventario.data.Data.Statements;

namespace inventario.data.Data
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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

        public async Task<IEnumerable<CategoriaModel>> ListarAsync(string nome, int ativo)
        {
            throw new NotImplementedException("Filtro por nome não está funcionando.");
            return await _unitOfWork.Connection
                            .QueryAsync<CategoriaModel>(sql: CategoriaStatements.ListarCategorias,
                                                       param: new { Nome = nome, Ativo = ativo },
                                                       transaction: _unitOfWork.Transaction);
        }

    }
}
