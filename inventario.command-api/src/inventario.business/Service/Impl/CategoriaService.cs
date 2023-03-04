using inventario.business.Abstractions.Data;
using inventario.business.Models;
using inventario.business.Models.Request;
using inventario.business.Models.Response;
using System;
using System.Collections.Generic;
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

        public async Task<CategoriaResponse> AdicionarAsync(CategoriaRequest request)
        {
            CategoriaModel categoriaModel = CreateFrom(request);

            await _categoriaRepository.AdicionarAsync(categoriaModel);

            return CreateFrom(categoriaModel);
        }

        public async Task<CategoriaResponse> AlterarAsync(Guid Id, CategoriaRequest request)
        {
            CategoriaModel categoriaModel = CreateFrom(request);

            await _categoriaRepository.AlterarAsync(categoriaModel);

            return CreateFrom(categoriaModel);
        }

        public Task<IEnumerable<CategoriaResponse>> ListarAsync(string nome, bool ativo)
        {
            throw new NotImplementedException();
        }

        private static CategoriaModel CreateFrom(CategoriaRequest request) => new()
        {
            Ativo = request.Ativo,
            Id = request.Id ?? Guid.NewGuid(),
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
