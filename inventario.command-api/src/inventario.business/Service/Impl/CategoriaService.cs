using inventario.business.Abstractions.Data;
using inventario.business.Models;
using inventario.business.Models.Request;
using inventario.business.Models.Response;
using System.Threading;
using System.Threading.Tasks;

namespace inventario.business.Service
{
    public class CategoriaService : ICategoriaService
    {
        private ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<CategoriaResponse> AdicionarAsync(CategoriaRequest request, CancellationToken cancellationToken = default)
        {
            CategoriaModel categoriaModel = CreateFrom(request);

            await _categoriaRepository.AdicionarAsync(categoriaModel, cancellationToken);

            return CreateFrom(categoriaModel);
        }

        public async Task<CategoriaResponse> AlterarAsync(CategoriaRequest request, CancellationToken cancellationToken = default)
        {
            CategoriaModel categoriaModel = CreateFrom(request);

            await _categoriaRepository.AlterarAsync(categoriaModel, cancellationToken);

            return CreateFrom(categoriaModel);
        }

        private static CategoriaModel CreateFrom(CategoriaRequest request) => new()
        {
            Ativo = request.Ativo,
            Id = request.Id,
            Nome = request.Nome,
        };

        private static CategoriaResponse CreateFrom(CategoriaModel model) => new()
        {
            Id = model.Id,
            Ativo = model.Ativo,
            Nome = model.Nome
        };
    }
}
