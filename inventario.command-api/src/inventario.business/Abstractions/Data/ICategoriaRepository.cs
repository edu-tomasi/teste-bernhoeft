using inventario.business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventario.business.Abstractions.Data
{
    public interface ICategoriaRepository
    {
        Task AdicionarAsync(CategoriaModel categoria);

        Task AlterarAsync(CategoriaModel categoria);

        Task<IEnumerable<CategoriaModel>> ListarAsync(Guid? id = null, string nome = null, bool? ativo = null);

        Task RemoverAsync(Guid Id);
    }
}
