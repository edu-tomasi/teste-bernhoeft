using inventario.business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventario.business.Abstractions.Data
{
    public interface ICategoriaRepository
    {
        Task AdicionarAsync(CategoriaModel categoria);

        Task AlterarAsync(CategoriaModel categoria);

        Task<IEnumerable<CategoriaModel>> ListarAsync(string nome, int ativo);
    }
}
