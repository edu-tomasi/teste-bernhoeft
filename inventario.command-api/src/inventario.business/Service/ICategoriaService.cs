using inventario.business.Models.Request;
using inventario.business.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventario.business.Service
{
    public interface ICategoriaService
    {
        Task<CategoriaResponse> AdicionarAsync(CategoriaRequest request);

        Task<CategoriaResponse> AlterarAsync(CategoriaRequest request);

        Task<IEnumerable<CategoriaResponse>> ListarAsync(string nome, bool ativo);
    }
}
