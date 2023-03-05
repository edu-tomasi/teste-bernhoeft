using inventario.business.Models;
using inventario.business.Models.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventario.business.Abstractions.Data
{
    public interface ICategoriaRepository
    {
        Task AdicionarAsync(CategoriaModel categoria);

        Task AlterarAsync(CategoriaModel categoria);

        Task<IEnumerable<CategoriaModel>> ListarAsync(FilterCategoriaRequest request);

        Task RemoverAsync(Guid id);
    }
}
