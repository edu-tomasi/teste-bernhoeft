using inventario.business.Abstractions.Data;
using inventario.business.Models;
using inventario.business.Models.Request;
using inventario.business.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventario.business.Service
{
    public class CategoriaService : ICategoriaService
    {
        private ICategoriaRepository _categoriaRepository;
        private IUnitOfWork _uow;

        public CategoriaService(ICategoriaRepository categoriaRepository, IUnitOfWork uow)
        {
            _categoriaRepository = categoriaRepository;
            _uow = uow;
        }

        public async Task<CategoriaResponse> AdicionarAsync(CategoriaRequest request)
        {
            CategoriaModel categoriaModel = CreateFrom(request);

            _uow.BeginTransaction();
            await _categoriaRepository.AdicionarAsync(categoriaModel);
            _uow.Commit();

            return CreateFrom(categoriaModel);
        }

        public async Task<CategoriaResponse> AlterarAsync(Guid Id, CategoriaRequest request)
        {
            CategoriaModel categoriaModel = CreateFrom(request);

            var categorias = await _categoriaRepository.ListarAsync(Id);

            if (!categorias.Any())
            {
                throw new NotImplementedException();
            }

            _uow.BeginTransaction();
            await _categoriaRepository.AlterarAsync(new() { Id = Id, Ativo = request.Ativo, Nome = request.Nome });
            _uow.Commit();

            return CreateFrom(categoriaModel);
        }

        public async Task RemoverAsync(Guid Id)
        {
            _uow.BeginTransaction();
            await _categoriaRepository.RemoverAsync(Id);
            _uow.Commit();
        }

        public async Task<IEnumerable<CategoriaResponse>> ListarAsync(Guid? id = null, string? nome = null, bool? ativo = null)
        {
            var result = await _categoriaRepository.ListarAsync(id, nome, ativo);

            return result.Select(s => CreateFrom(s));
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
